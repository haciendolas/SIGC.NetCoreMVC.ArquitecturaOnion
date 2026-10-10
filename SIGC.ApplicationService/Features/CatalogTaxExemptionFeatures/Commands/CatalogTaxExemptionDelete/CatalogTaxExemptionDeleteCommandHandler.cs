using MediatR;
using SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Commands.CatalogTaxExemptionDelete
{
    internal class CatalogTaxExemptionDeleteCommandHandler(
        ICatalogTaxExemptionDeleteRepository CatalogTaxExemptionDeleteRepository,
        ICurrentSessionService CurrentSessionService,
        IMessageService MessageService
    ) : IRequestHandler<CatalogTaxExemptionDeleteCommandRequest, MsgResponse<object?>>
    {
        public async Task<MsgResponse<object?>> Handle(CatalogTaxExemptionDeleteCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try{
                var RecordAffected = await CatalogTaxExemptionDeleteRepository.DeleteAsync(CurrentSessionService.CompanyID, Request.CatalogTaxExemptionID, CancellationToken);
                if (RecordAffected > 0)
                {
                    MsgResponse.Type = MessageTypeConst.SUCCESS;                    
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_DELETE);                
                }
                else{
                    MsgResponse.Type = MessageTypeConst.ERROR;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_DELETE);
                }
            }
            catch(Exception ex)
            {
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";
            }
            return MsgResponse;
        }
    }
}