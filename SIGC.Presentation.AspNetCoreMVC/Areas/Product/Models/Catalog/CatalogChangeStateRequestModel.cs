namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Catalog
{
    public sealed record CatalogChangeStateRequestModel
    (
         int CatalogID,
         byte RecordStateID
    );
}