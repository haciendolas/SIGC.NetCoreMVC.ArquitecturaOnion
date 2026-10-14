using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogStockFeatures.Commands.CatalogStockCreate
{
    public sealed record CatalogStockCreateCommandRequest
    (
        int CatalogVariantID,
        int WarehouseID,
        decimal CatalogStockCurrentQuantity,
        decimal CatalogStockMinimumQuantity,
        decimal CatalogStockMaximumQuantity,       
        RecordOriginEnum RecordOriginID,
        RecordStateEnum RecordStateID 
    ):IRequest<MsgResponse<object>>;
}
