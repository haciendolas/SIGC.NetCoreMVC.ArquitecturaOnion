namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPresentation
{
    public sealed record CatalogVariantListResponseModel
    (
        int CatalogVariantID,
        string CatalogVariantName,
        List<CatalogPresentationListResponseModel> CatalogPresentations
    );

    public sealed record CatalogPresentationListResponseModel
    (
        int CatalogPresentationID,
        string CatalogPresentationName
    );
}