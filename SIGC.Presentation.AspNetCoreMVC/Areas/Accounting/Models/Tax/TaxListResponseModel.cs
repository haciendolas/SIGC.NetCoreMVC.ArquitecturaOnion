namespace SIGC.Presentation.AspNetCoreMVC.Areas.Accounting.Models.Tax
{
    public sealed record TaxListResponseModel
    (
        short TaxID,
        string TaxCode,
        string TaxName,
        decimal TaxValor
    ); 
}