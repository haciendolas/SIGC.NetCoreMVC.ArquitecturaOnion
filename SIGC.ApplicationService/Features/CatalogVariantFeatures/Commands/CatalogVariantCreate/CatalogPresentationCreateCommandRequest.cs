using SIGC.DomainModel.Enums;

namespace SIGC.ApplicationService.Features.CatalogVariantFeatures.Commands.CatalogVariantCreate
{
    public sealed record CatalogPresentationCreateCommandRequest
    (
        int PresentationID,
        bool CatalogPresentationIsDefault,
        decimal CatalogPresentationEquivalence,
        string? CatalogPresentationSKU,
        string? CatalogPresentationBarcode,
        RecordStateEnum RecordStateID
    );    
}