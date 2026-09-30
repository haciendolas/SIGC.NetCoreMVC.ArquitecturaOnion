namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogVariant
{
    public sealed record CatalogVariantChangeStateRequestModel
    (
         int CatalogVariantID,
         byte RecordStateID
    );
}