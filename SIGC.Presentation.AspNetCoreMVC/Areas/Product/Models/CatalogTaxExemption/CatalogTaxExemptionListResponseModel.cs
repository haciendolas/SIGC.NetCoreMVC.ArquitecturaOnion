namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTaxExemption
{
    public sealed record CatalogTaxExemptionListResponseModel
    (
        int CatalogTaxExemptionID,
        int CatalogTaxID,
        string EstablishmentName,
        byte RecordStateID
    );    
}