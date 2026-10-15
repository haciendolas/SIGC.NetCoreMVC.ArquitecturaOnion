using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogStockFeatures.Commands.CatalogStockUpdate
{
    public sealed record CatalogStockUpdateCommandRequest
    (   int CatalogStockID,
        int CatalogPriceID,
        int CatalogVariantID,
        int WarehouseID,
        decimal? CatalogStockCurrentQuantity,
        decimal CatalogStockMinimumQuantity,
        decimal CatalogStockMaximumQuantity,
        RecordStateEnum RecordStateID
    ) :IRequest<MsgResponse<object>>;
}
