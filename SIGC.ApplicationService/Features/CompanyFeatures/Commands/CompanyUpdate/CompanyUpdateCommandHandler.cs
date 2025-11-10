using MediatR;
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
      
        public CompanyUpdateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            ICompanyUpdateRepository CompanyUpdateRepository,
            ICompanyVerifyDocumentNumberAndSocialReasonRepository CompanyVerifyDocumentNumberAndSocialReasonRepository         
            )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.CompanyUpdateRepository = CompanyUpdateRepository;
            this.CompanyVerifyDocumentNumberAndSocialReasonRepository = CompanyVerifyDocumentNumberAndSocialReasonRepository;            
        }

        public async Task<MsgResponse<object?>> Handle(CompanyUpdateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
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
                   
                    int RecordAffected = await CompanyUpdateRepository.UpdateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    { 
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
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";

            }
            return MsgResponse;
        }  
    }
}