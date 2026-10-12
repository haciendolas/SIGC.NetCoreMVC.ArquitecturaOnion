namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPresentation
{
    public sealed record CatalogPresentationListResponseModel
    (
        int CatalogVariantID,
        string CatalogVariantName,
        List<CatalogPresentationItemListResponseModel> CatalogPresentations
    );

    public sealed record CatalogPresentationItemListResponseModel
    (
        int CatalogPresentationID,
        string CatalogPresentationName
    );
}