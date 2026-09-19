namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogVariant
{
    public sealed record CatalogVariantListResponseModel
    (
        int CatalogVariantID,
        string CatalogVariantName,
        string CatalogVariantSKU,
        byte CatalogVariantStateID,
        List<CatalogVariantValueListResponseModel> CatalogVariantValues,
        List<CatalogPresentationListResponseModel> CatalogPresentations
    );

    public sealed record CatalogPresentationListResponseModel(
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

    public sealed record CatalogVariantValueListResponseModel
    (
        short AttributeValueID,
        string AttributeName,
        string AttributeValueName
    );
}
