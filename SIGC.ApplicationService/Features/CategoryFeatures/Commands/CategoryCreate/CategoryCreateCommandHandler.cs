using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryCreate
{
    internal class CategoryCreateCommandHandler(
        ICategoryCreateRepository CategoryCreateRepository,
        ICategoryVerifyNameRepository CategoryValidateRepository,
        IMessageService MessageService,
        ICurrentSessionService CurrentSessionService,
        IFileStorageService FileStorageService,
        FileUploadSettings FileUploadSettings
    ) : IRequestHandler<CategoryCreateCommandRequest, MsgResponse<object>>
    { 
        public async Task<MsgResponse<object>> Handle(CategoryCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();
            FileEntryDto FileEntry = new FileEntryDto("", "");
            try
            {
                var Model = Category.Create(
                        CurrentSessionService.CompanyID,
                        Request.CategoryName,
                        Request.CategorySlug,
                        Request.File == null ? null : $"{Request.CategoryName}{Path.GetExtension(Request.File.FileName)}",
                        Request.RecordStateId, 
                        DateTime.Now, 
                        CurrentSessionService.UserID,
                        CurrentSessionService.UserName,
                        CurrentSessionService.UserFullName
                    );

                var Validate = await CategoryValidateRepository.VerifyNameAsync(Model, CancellationToken);
                if (Validate == VerifyRegistryConst.Category.OK)
                {
                    int RecordAffected = await CategoryCreateRepository.CreateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {
                        if (Request.File is not null)
                        {
                            FileEntry.FileName = Model.CategoryImage;
                            FileEntry.FileLocation = $"{FileUploadSettings.CategoryImageLocation}/{Model.CategoryImage}";
                            await FileStorageService.CreateAsync(FileEntry, Request.File.OpenReadStream(), CancellationToken);
                        }

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.CategoryId,
                            Model.CategoryName,
                            Model.RecordStateId,
                            Model.CreatedDate,
                        };
                    }
                    else
                    {
                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_INSERT);
                    }
                }
                else{
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATEGORY_CATEGORYNAME);
                }
            }
            catch (ArgumentNullException ae)
            {
                MsgResponse.Type = MessageTypeConst.WARNING;
                MsgResponse.Message = ae.Message;
            }
            catch (Exception ex)
            {
                if (Request.File is not null && !string.IsNullOrWhiteSpace(FileEntry.FileName)) await FileStorageService.DeleteAsync(FileEntry, CancellationToken);

                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";

            }
            return MsgResponse;
        }
    }
}
