using MediatR;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPresentationFeatures.Queries.CatalogPresentationList
{
    public sealed record CatalogPresentationListQueryRequest(int CatalogID
    ) :IRequest<MsgResponse<List<CatalogVariantListQueryResponse>>>;
}