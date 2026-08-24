using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogRepositories;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogCreate
{
    internal class CatalogCreateCommandHandler(
        ICatalogCreateRepository CatalogCreateRepository,
        ICategoryVerifyNameRepository CatalogValidateRepository,
        IMessageService MessageService,
        ICurrentSessionService CurrentSessionService,
        IFileStorageService FileStorageService,
        FileUploadSettings FileUploadSettings
    ) : IRequestHandler<CatalogCreateCommandRequest, MsgResponse<object>>
    { 
        public async Task<MsgResponse<object>> Handle(CatalogCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();
            FileEntryDto FileEntry = new FileEntryDto("", "");
            try
            {
                var CurrentDate = DateTime.Now;
                var Model = Catalog.Create(
                        CurrentSessionService.CompanyID,
                        Request.CatalogTypeID,
                        Request.CategoryID,
                        Request.CatalogCode,
                        Request.CatalogSlug,
                        Request.CatalogName,
                        Request.PrescriptionTypeID,
                        Request.ManufacturerID,
                        Request.BrandID,
                        Request.PharmaceuticalFormID,
                        Request.CatalogBrandType,
                        Request.CatalogDescription,
                        Request.File == null ? null : $"{CurrentDate.ToString("ddMMyyyyHHmmss")}_{Path.GetExtension(Request.File.FileName)}",
                        Request.RecordOriginID,
                        Request.RecordStateID,                    
                        CurrentDate, 
                        CurrentSessionService.UserID,
                        CurrentSessionService.UserName,
                        CurrentSessionService.UserFullName
                    );

                var Validate = "OK";//await CatalogValidateRepository.VerifyNameAsync(Model, CancellationToken);
                if (Validate == VerifyRegistryConst.Catalog.OK)
                {
                    int RecordAffected = await CatalogCreateRepository.CreateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {
                        if (Request.File is not null)
                        {
                            FileEntry.FileName = Model.CatalogImage;
                            FileEntry.FileLocation = $"{FileUploadSettings.CatalogImageLocation}/{Model.CatalogImage}";
                            await FileStorageService.CreateAsync(FileEntry, Request.File.OpenReadStream(), CancellationToken);
                        }

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.CatalogID,
                            Model.CatalogImage,
                            Model.RecordStateID,
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
