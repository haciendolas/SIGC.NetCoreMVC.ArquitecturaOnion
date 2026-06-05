using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyUpdate
{
    internal class CompanyUpdateCommandHandler : IRequestHandler<CompanyUpdateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly ICompanyUpdateRepository CompanyUpdateRepository;
        private readonly ICompanyVerifyDocumentNumberAndSocialReasonRepository CompanyVerifyDocumentNumberAndSocialReasonRepository;
        private readonly IFileStorageService FileStorageService;
        private readonly FileUploadSettings FileUploadSettings;

        public CompanyUpdateCommandHandler(
            FileUploadSettings FileUploadSettings,
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            ICompanyUpdateRepository CompanyUpdateRepository,
            ICompanyVerifyDocumentNumberAndSocialReasonRepository CompanyVerifyDocumentNumberAndSocialReasonRepository,
            IFileStorageService FileStorageService
            )
        {
            this.FileUploadSettings = FileUploadSettings;
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.CompanyUpdateRepository = CompanyUpdateRepository;
            this.CompanyVerifyDocumentNumberAndSocialReasonRepository = CompanyVerifyDocumentNumberAndSocialReasonRepository;
            this.FileStorageService = FileStorageService;
        }

        public async Task<MsgResponse<object?>> Handle(CompanyUpdateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            FileEntryDto FileEntry = new FileEntryDto("", "");
            try
            {
                var Model = Company.Update(
                        Request.CompanyID,
                        Request.CompanyTradeName,
                        Request.CompanySocialReason,
                        Request.CompanyDocumentNumber,
                        Request.CompanyBirthDate,
                        Request.CountryID,
                        Request.CompanyAddress,
                        Request.TaxpayerTypeID,
                        Request.RubroID,
                        Request.CompanyCorporateEmail,
                        Request.CompanyMobile,
                        Request.CompanyPhone,
                        Request.CompanyLogoBandera == "DELETE" ? null :  Request.File == null ? Request.CompanyLogo : $"{Request.CompanyDocumentNumber}{Path.GetExtension(Request.File.FileName)}",
                        Request.StateID,
                        DateTime.Now,
                       CurrentSessionService.UserID
                    );

                var Verify = await CompanyVerifyDocumentNumberAndSocialReasonRepository.VerifyDocumentNumberAndSocialAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.Company.OK)
                {                   
                    int RecordAffected = await CompanyUpdateRepository.UpdateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {
                        if (Request.File is not null)
                        {
                            if (!string.IsNullOrWhiteSpace(Request.CompanyLogo))
                            {
                                FileEntry.FileName = Request.CompanyLogo;
                                FileEntry.FileLocation = $"{FileUploadSettings.CompanyLogoLocation}/{Request.CompanyLogo}";
                                await FileStorageService.DeleteAsync(FileEntry, CancellationToken);
                            }

                            FileEntry.FileName = Model.CompanyLogo;
                            FileEntry.FileLocation = $"{FileUploadSettings.CompanyLogoLocation}/{Model.CompanyLogo}";
                            await FileStorageService.CreateAsync(FileEntry, Request.File.OpenReadStream(), CancellationToken);
                        }

                        if(Request.CompanyLogoBandera == "DELETE")
                        {  
                             FileEntry.FileName = Request.CompanyLogo;
                             FileEntry.FileLocation = $"{FileUploadSettings.CompanyLogoLocation}/{Request.CompanyLogo}";
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
                    MsgResponse.Message = MessageService.GetMessageResult(Verify == VerifyRegistryConst.Company.DOCUMENT_NUMBER_EXISTS ? MessageDescriptionConst.EXIST_COMPANY_DOCUMENTNUMBER : MessageDescriptionConst.EXIST_COMPANY_SOCIALREASON);
                }
            }
            catch (ArgumentNullException ae)
            {
                MsgResponse.Type = MessageTypeConst.WARNING;
                MsgResponse.Message = "El número de documento es obligatorio";
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