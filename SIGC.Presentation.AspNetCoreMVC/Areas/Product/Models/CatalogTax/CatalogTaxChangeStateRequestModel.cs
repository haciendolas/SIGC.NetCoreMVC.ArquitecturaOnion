namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTax
{
    public sealed record CatalogTaxChangeStateRequestModel
    (
         int CatalogTaxID,
         byte RecordStateID
    );
}