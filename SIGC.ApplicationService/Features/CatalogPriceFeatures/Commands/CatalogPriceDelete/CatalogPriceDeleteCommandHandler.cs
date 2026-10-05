using MediatR;
using SIGC.DomainService.IRepositories.ICatalogPriceRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPriceFeatures.Commands.CatalogPriceDelete
{
    internal class CatalogPriceDeleteCommandHandler(
        ICatalogPriceDeleteRepository CatalogPriceDeleteRepository,
        ICurrentSessionService CurrentSessionService,
        IMessageService MessageService
    ) : IRequestHandler<CatalogPriceDeleteCommandRequest, MsgResponse<object?>>
    {
        public async Task<MsgResponse<object?>> Handle(CatalogPriceDeleteCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try{
                var RecordAffected = await CatalogPriceDeleteRepository.DeleteAsync(CurrentSessionService.CompanyID, Request.CatalogPriceID, CancellationToken);
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