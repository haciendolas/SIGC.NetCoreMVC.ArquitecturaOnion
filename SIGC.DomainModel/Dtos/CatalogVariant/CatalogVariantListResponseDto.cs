namespace SIGC.DomainModel.Dtos.CatalogVariant
{
    public sealed record CatalogVariantListResponseDto
    (
        int CatalogVariantID,
        string CatalogVariantName,
        string CatalogVariantSKU,
        byte CatalogVariantStateID,
        List<CatalogVariantValueListResponseDto> CatalogVariantValues,
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

    public sealed record CatalogVariantValueListResponseDto
    (
        short AttributeValueID,
        string AttributeName,
        string AttributeValueName
     );
}