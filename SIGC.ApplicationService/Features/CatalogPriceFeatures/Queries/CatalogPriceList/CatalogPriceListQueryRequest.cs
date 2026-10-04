using MediatR;
using SIGC.DomainModel.Dtos.CatalogPrice;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPriceFeatures.Queries.CatalogPriceList
{
    public sealed record CatalogPriceListQueryRequest(
        int CatalogID
    ) :IRequest<MsgResponse<List<CatalogPriceListResponseDto>>>;
}