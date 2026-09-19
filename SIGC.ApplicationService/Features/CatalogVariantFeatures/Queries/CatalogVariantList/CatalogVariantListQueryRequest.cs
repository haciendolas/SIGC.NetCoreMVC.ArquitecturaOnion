using MediatR;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogVariantFeatures.Queries.CatalogVariantList
{
    public sealed record CatalogVariantListQueryRequest(int CatalogID
    ) :IRequest<MsgResponse<List<CatalogVariantListQueryResponse>>>;
}