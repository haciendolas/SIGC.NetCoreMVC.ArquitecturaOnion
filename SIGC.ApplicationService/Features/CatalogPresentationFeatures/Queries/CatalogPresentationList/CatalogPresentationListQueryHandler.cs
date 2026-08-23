using MediatR;
using SIGC.DomainService.IRepositories.ICatalogPresentationRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPresentationFeatures.Queries.CatalogPresentationList
{
    internal class CatalogPresentationListQueryHandler : IRequestHandler<CatalogPresentationListQueryRequest, MsgResponse<List<CatalogVariantListQueryResponse>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly ICatalogPresentationListRepository CatalogPresentationListRepository;

        public CatalogPresentationListQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            ICatalogPresentationListRepository CatalogPresentationListRepository
            ) { 
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.CatalogPresentationListRepository = CatalogPresentationListRepository;        
        }

        public async Task<MsgResponse<List<CatalogVariantListQueryResponse>>> Handle(CatalogPresentationListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<CatalogVariantListQueryResponse>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            var CatalogPresentationList = await CatalogPresentationListRepository.ListAsync(CurrentSessionService.CompanyID,Request.CatalogID,CancellationToken);

            MsgResponse.Data = CatalogPresentationList.GroupBy(x => new { x.CatalogVariantID, x.CatalogVariantName})
                                .Select(g => new CatalogVariantListQueryResponse(
                                    g.Key.CatalogVariantID,
                                    g.Key.CatalogVariantName,                               
                                    g.Select(x => new CatalogPresentationListQueryResponse(
                                        x.CatalogPresentationID,
                                        x.CatalogPresentationName
                                    )).ToList()
                                )).ToList();
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
