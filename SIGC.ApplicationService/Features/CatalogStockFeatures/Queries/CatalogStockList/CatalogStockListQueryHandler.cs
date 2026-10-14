using MediatR;
using SIGC.DomainModel.Dtos.CatalogStock; 
using SIGC.DomainService.IRepositories.ICatalogStockRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogStockFeatures.Queries.CatalogStockList
{
    internal class CatalogStockListQueryHandler : IRequestHandler<CatalogStockListQueryRequest, MsgResponse<List<CatalogStockListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly ICatalogStockListRepository CatalogStockListRepository;

        public CatalogStockListQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            ICatalogStockListRepository CatalogStockListRepository
            ) { 
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.CatalogStockListRepository = CatalogStockListRepository;        
        }

        public async Task<MsgResponse<List<CatalogStockListResponseDto>>> Handle(CatalogStockListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<CatalogStockListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await CatalogStockListRepository.ListAsync(CurrentSessionService.CompanyID,Request.CatalogID,Request.EstablishmentID,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
