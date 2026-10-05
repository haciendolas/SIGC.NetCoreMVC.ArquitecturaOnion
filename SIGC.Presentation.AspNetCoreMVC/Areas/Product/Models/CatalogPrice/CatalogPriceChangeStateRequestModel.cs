namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPrice
{
    public sealed record CatalogPriceChangeStateRequestModel
    (
         int CatalogPriceID,
         byte RecordStateID
    );
}