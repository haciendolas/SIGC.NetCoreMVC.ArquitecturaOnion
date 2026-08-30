using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Models;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.ICompanyRegisterRepositories;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.DomainService.Transactions;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyCreate
{
    internal class CompanyCreateCommandHandler : IRequestHandler<CompanyCreateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly ICompanyCreateRepository CompanyCreateRepository;
        private readonly ICompanyVerifyDocumentNumberAndSocialReasonRepository CompanyVerifyDocumentNumberAndSocialReasonRepository;
        private readonly IUnitOfWork UnitOfWork;
        private readonly IFileStorageService FileStorageService;
        private readonly ICompanyRegisterCreateRepository CompanyRegisterCreateRepository;
        private readonly FileUploadSettings FileUploadSettings;

        public CompanyCreateCommandHandler(
            FileUploadSettings FileUploadSettings,
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            ICompanyCreateRepository CompanyCreateRepository,
            ICompanyVerifyDocumentNumberAndSocialReasonRepository CompanyVerifyDocumentNumberAndSocialReasonRepository, 
            IUnitOfWork UnitOfWork,
            IFileStorageService FileStorageService,
            ICompanyRegisterCreateRepository CompanyRegisterCreateRepository
            )
        {
            this.FileUploadSettings = FileUploadSettings;
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.CompanyCreateRepository = CompanyCreateRepository;
            this.CompanyVerifyDocumentNumberAndSocialReasonRepository = CompanyVerifyDocumentNumberAndSocialReasonRepository; 
            this.UnitOfWork = UnitOfWork;
            this.FileStorageService = FileStorageService;
            this.CompanyRegisterCreateRepository = CompanyRegisterCreateRepository;
        }

        public async Task<MsgResponse<object?>> Handle(CompanyCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            FileEntryDto FileEntry = new FileEntryDto("","");
            try{
                var Model = Company.Create(
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
                        Request.File == null ? null :$"{Request.CompanyDocumentNumber}{Path.GetExtension(Request.File.FileName)}",
                        Request.StateID,
                        DateTime.Now,
                       CurrentSessionService.UserID
                    );

                await UnitOfWork.BeginTransactionAsync(CancellationToken);

                var Verify = await CompanyVerifyDocumentNumberAndSocialReasonRepository.VerifyDocumentNumberAndSocialAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.Company.OK)
                {
                    int RecordAffected = await CompanyCreateRepository.CreateAsync(Model, CancellationToken);
                        RecordAffected = await CompanyRegisterCreateRepository.CreateAsync(new CompanyRegister{
                                                CompanyID = Model.CompanyID,
                                                CompanyIDRegister =CurrentSessionService.CompanyID,
                                                CompanyRegisterCreatedDateTime = Model.CreatedDateTime,
                                                CompanyRegisterCreatedUserID = Model.CreatedBy
                                            }, CancellationToken);

                    if (RecordAffected > 0)
                    {
                        if (Request.File is not null)
                        {
                            FileEntry.FileName = Model.CompanyLogo;
                            FileEntry.FileLocation = $"{FileUploadSettings.CompanyLogoLocation}/{Model.CompanyLogo}";
                            await FileStorageService.CreateAsync(FileEntry, Request.File.OpenReadStream(), CancellationToken);
                        }

                        await UnitOfWork.CommitTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_INSERT);                        
                    }
                    else
                    {
                        await UnitOfWork.RollbackTransactionAsync(CancellationToken);

                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_INSERT);
                    }
                }
                else
                {
                    await UnitOfWork.RollbackTransactionAsync(CancellationToken);

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
                await UnitOfWork.RollbackTransactionAsync(CancellationToken);

                if(Request.File is not null && !string.IsNullOrWhiteSpace(FileEntry.FileName)) await FileStorageService.DeleteAsync(FileEntry, CancellationToken);
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";

            }
            return MsgResponse;
        }
    }    
}
