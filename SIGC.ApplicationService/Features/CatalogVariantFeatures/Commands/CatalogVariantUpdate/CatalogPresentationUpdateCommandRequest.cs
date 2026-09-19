using SIGC.DomainModel.Enums;

namespace SIGC.ApplicationService.Features.CatalogVariantFeatures.Commands.CatalogVariantUpdate
{
    public sealed record CatalogPresentationUpdateCommandRequest
    (
        int CatalogPresentationID,
        int PresentationID, 
        decimal CatalogPresentationEquivalence,
        string? CatalogPresentationSKU,
        string? CatalogPresentationBarcode,
        RecordStateEnum RecordStateID
    );    
}