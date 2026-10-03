using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPriceFeatures.Commands.CatalogPriceCreate
{
    public sealed record CatalogPriceCreateCommandRequest
    (
        int CatalogPresentationID,
        int EstablishmentID,
        PriceTypeEnum PriceTypeID,
        CurrencyTypeEnum CurrencyTypeID,
        decimal CatalogPriceAmount,
        bool  CatalogPriceIsTaxIncluded,
        RecordOriginEnum RecordOriginID,
        RecordStateEnum RecordStateID 
    ):IRequest<MsgResponse<object>>;
}
