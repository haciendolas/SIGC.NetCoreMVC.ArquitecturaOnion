using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPresentationFeatures.Commands.CatalogPresentationCreate
{
    public sealed record CatalogPresentationCreateCommandRequest
    (
        int CatalogVariantID,
        int PresentationID,
        bool CatalogPresentationIsDefault,
        decimal CatalogPresentationEquivalence,
        string? CatalogPresentationSKU,
        string? CatalogPresentationBarcode,
        RecordOriginEnum RecordOriginID ,
        RecordStateEnum RecordStateID 
    ):IRequest<MsgResponse<object>>;
}
