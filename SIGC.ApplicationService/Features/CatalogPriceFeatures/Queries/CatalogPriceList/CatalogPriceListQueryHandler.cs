using MediatR;
using SIGC.DomainModel.Dtos.CatalogPrice; 
using SIGC.DomainService.IRepositories.ICatalogPriceRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPriceFeatures.Queries.CatalogPriceList
{
    internal class CatalogPriceListQueryHandler : IRequestHandler<CatalogPriceListQueryRequest, MsgResponse<List<CatalogPriceListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly ICatalogPriceListRepository CatalogPriceListRepository;

        public CatalogPriceListQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            ICatalogPriceListRepository CatalogPriceListRepository
            ) { 
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.CatalogPriceListRepository = CatalogPriceListRepository;        
        }

        public async Task<MsgResponse<List<CatalogPriceListResponseDto>>> Handle(CatalogPriceListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<CatalogPriceListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await CatalogPriceListRepository.ListAsync(CurrentSessionService.CompanyID,Request.CatalogID,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
