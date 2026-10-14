using MediatR;
using SIGC.DomainModel.Dtos.CatalogStock;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogStockFeatures.Queries.CatalogStockList
{
    public sealed record CatalogStockListQueryRequest(
        int CatalogID,
        int? EstablishmentID
    ) :IRequest<MsgResponse<List<CatalogStockListResponseDto>>>;
}