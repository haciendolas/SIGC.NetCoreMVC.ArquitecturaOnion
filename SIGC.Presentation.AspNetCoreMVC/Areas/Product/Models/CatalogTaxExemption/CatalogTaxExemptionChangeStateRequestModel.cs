namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTaxExemption
{
    public sealed record CatalogTaxExemptionChangeStateRequestModel
    (
         int CatalogTaxExemptionID,
         byte RecordStateID
    );
}