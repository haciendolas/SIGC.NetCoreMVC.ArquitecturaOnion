namespace SIGC.ApplicationService.Features.CatalogVariantFeatures.Queries.CatalogVariantList
{
    public sealed record CatalogVariantListQueryResponse(
        int CatalogVariantID,
        string CatalogVariantName,  
        string CatalogVariantSKU,
        byte CatalogVariantStateID,
        List<CatalogVariantValueListQueryResponse> CatalogVariantValues,
        List<CatalogPresentationListQueryResponse> CatalogPresentations
    );

    public sealed record CatalogPresentationListQueryResponse(
        int UnitMeasureID,
        string UnitMeasureName,
        int CatalogPresentationID, 
        int PresentationID,
        string PresentationName,
        bool CatalogPresentationIsDefault,
        decimal CatalogPresentationEquivalence,
        string CatalogPresentationSKU,
        string CatalogPresentationBarcode,
        byte CatalogPresentationStateID
    );

    public sealed record CatalogVariantValueListQueryResponse
    (
        short AttributeValueID,
        string AttributeName,
        string AttributeValueName
    );
}