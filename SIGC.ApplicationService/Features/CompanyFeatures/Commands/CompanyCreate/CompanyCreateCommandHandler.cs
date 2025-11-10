using MediatR;
using SIGC.DomainModel.Models;
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
        public CompanyCreateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            ICompanyCreateRepository CompanyCreateRepository,
            ICompanyVerifyDocumentNumberAndSocialReasonRepository CompanyVerifyDocumentNumberAndSocialReasonRepository, 
            IUnitOfWork UnitOfWork
            )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.CompanyCreateRepository = CompanyCreateRepository;
            this.CompanyVerifyDocumentNumberAndSocialReasonRepository = CompanyVerifyDocumentNumberAndSocialReasonRepository; 
            this.UnitOfWork = UnitOfWork;
        }

        public async Task<MsgResponse<object?>> Handle(CompanyCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = Company.Create(
                        Request.CompanyTradeName,
                        Request.CompanySocialReason,
                        Request.CompanyDocumentNumber,
                        Request.CompanyBirthDate,
                        Request.CountryID,
                        Request.CompanyAddress,
                        Request.TaxpayerTypeID,
                        Request.RubroID,
                        Request.CompanyMobile,
                        Request.CompanyPhone,
                        Request.CompanyLogo,
                        Request.StateID,
                        DateTime.Now,
                       CurrentSessionService.UserID
                    );

                var Verify = await CompanyVerifyDocumentNumberAndSocialReasonRepository.VerifyDocumentNumberAndSocialAsync(Model, CancellationToken);
                if (Verify == VerifyRegistryConst.Company.OK)
                {
                    await UnitOfWork.BeginTransactionAsync(CancellationToken);
                    int RecordAffected = await CompanyCreateRepository.CreateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    { 
                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_INSERT);                        

                        await UnitOfWork.CommitTransactionAsync(CancellationToken);
                    }
                    else
                    {
                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_INSERT);
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
                MsgResponse.Message = "El codigo de rol es obligatorio";
            }
            catch (Exception ex)
            {
                await UnitOfWork.RollbackTransactionAsync(CancellationToken);
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";

            }
            return MsgResponse;
        }
    }    
}
