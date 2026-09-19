using MediatR;
using SIGC.DomainService.IRepositories.ICatalogVariantRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogVariantFeatures.Queries.CatalogVariantList
{
    internal class CatalogVariantListQueryHandler : IRequestHandler<CatalogVariantListQueryRequest, MsgResponse<List<CatalogVariantListQueryResponse>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly ICatalogVariantListRepository CatalogVariantListRepository;

        public CatalogVariantListQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            ICatalogVariantListRepository CatalogVariantListRepository
            ) { 
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.CatalogVariantListRepository = CatalogVariantListRepository;        
        }

        public async Task<MsgResponse<List<CatalogVariantListQueryResponse>>> Handle(CatalogVariantListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<CatalogVariantListQueryResponse>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            var CatalogVariantList = await CatalogVariantListRepository.ListAsync(CurrentSessionService.CompanyID,Request.CatalogID,CancellationToken);

            MsgResponse.Data = CatalogVariantList.GroupBy(x => new { x.CatalogVariantID, x.CatalogVariantName,x.CatalogVariantSKU,x.CatalogVariantStateID})
                                .Select(g => new CatalogVariantListQueryResponse(
                                    g.Key.CatalogVariantID,
                                    g.Key.CatalogVariantName,  
                                    g.Key.CatalogVariantSKU,
                                    g.Key.CatalogVariantStateID,
                                    g.SelectMany(x => x.CatalogVariantValues.
                                         Select(cv=> new CatalogVariantValueListQueryResponse(
                                            cv.AttributeValueID,
                                            cv.AttributeName,
                                            cv.AttributeValueName
                                         )
                                        )).ToList(),
                                    g.Select(x => new CatalogPresentationListQueryResponse(
                                        x.UnitMeasureID,
                                        x.UnitMeasureName,
                                        x.CatalogPresentationID,
                                        x.PresentationID,
                                        x.PresentationName,
                                        x.CatalogPresentationIsDefault,
                                        x.CatalogPresentationEquivalence,
                                        x.CatalogPresentationSKU,
                                        x.CatalogPresentationBarcode,
                                        x.CatalogPresentationStateID
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
