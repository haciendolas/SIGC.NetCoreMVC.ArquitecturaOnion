using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPriceFeatures.Commands.CatalogPriceUpdate
{
    public sealed record CatalogPriceUpdateCommandRequest
    (   int CatalogPriceID,
        int CatalogPresentationID,
        int EstablishmentID,
        PriceTypeEnum PriceTypeID,
        CurrencyTypeEnum CurrencyTypeID,
        decimal CatalogPriceAmount,
        bool  CatalogPriceIsTaxIncluded,       
        RecordStateEnum RecordStateID 
    ):IRequest<MsgResponse<object>>;
}
