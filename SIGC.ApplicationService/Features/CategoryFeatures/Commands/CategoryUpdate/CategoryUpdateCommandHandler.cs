using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryUpdate
{
    internal class CategoryUpdateCommandHandler(
        ICategoryUpdateRepository CategoryUpdateRepository,
        ICategoryVerifyNameRepository CategoryValidateRepository,
        ICurrentSessionService CurrentSessionService,
        IMessageService MessageService,
        IFileStorageService FileStorageService,
        FileUploadSettings FileUploadSettings
     ) : IRequestHandler<CategoryUpdateCommandRequest, MsgResponse<object?>>
     {
        public async Task<MsgResponse<object?>> Handle(CategoryUpdateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            FileEntryDto FileEntry = new FileEntryDto("", "");
            try
            {
                var Model = Category.Update(
                    CurrentSessionService.CompanyID,
                    Request.CategoryId,
                    Request.CategoryName,
                    Request.CategorySlug,
                    Request.CategoryImageBandera == "DELETE" ? null : Request.File == null ? Request.CategoryImage : $"{Request.CategoryName}{Path.GetExtension(Request.File.FileName)}",
                    Request.RecordStateId,
                    DateTime.Now,
                    CurrentSessionService.UserID,
                    CurrentSessionService.UserName,
                    CurrentSessionService.UserName
                    );

                var Validate = await CategoryValidateRepository.VerifyNameAsync(Model,CancellationToken);
                if (Validate == VerifyRegistryConst.Category.OK)
                {
                    var RecordAffected = await CategoryUpdateRepository.UpdateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {
                        if (Request.File is not null)
                        {
                            if (!string.IsNullOrWhiteSpace(Request.CategoryImage))
                            {
                                FileEntry.FileName = Request.CategoryImage;
                                FileEntry.FileLocation = $"{FileUploadSettings.CategoryImageLocation}/{Request.CategoryImage}";
                                await FileStorageService.DeleteAsync(FileEntry, CancellationToken);
                            }

                            FileEntry.FileName = Model.CategoryImage;
                            FileEntry.FileLocation = $"{FileUploadSettings.CategoryImageLocation}/{Model.CategoryImage}";
                            await FileStorageService.CreateAsync(FileEntry, Request.File.OpenReadStream(), CancellationToken);
                        }

                        if (Request.CategoryImageBandera == "DELETE")
                        {
                            FileEntry.FileName = Request.CategoryImage;
                            FileEntry.FileLocation = $"{FileUploadSettings.CategoryImageLocation}/{Request.CategoryImage}";
                            await FileStorageService.DeleteAsync(FileEntry, CancellationToken);
                        }
                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_UPDATE);
                    }
                    else
                    {
                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_UPDATE);
                    }
                }
                else
                {
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATEGORY_CATEGORYNAME);
                }
            }
            catch(Exception ex)
            {
                if (Request.File is not null && !string.IsNullOrWhiteSpace(FileEntry.FileName)) await FileStorageService.DeleteAsync(FileEntry, CancellationToken);

                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";
            }
            return MsgResponse;
        }
    }
}