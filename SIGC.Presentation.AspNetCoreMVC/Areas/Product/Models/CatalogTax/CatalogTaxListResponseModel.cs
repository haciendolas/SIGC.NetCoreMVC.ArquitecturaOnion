namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTax
{
    public sealed record CatalogTaxListResponseModel
    (
        int CatalogTaxID,
        short TaxID,
        string TaxName,
        decimal TaxValor,
        string CalculationTypeName,
        byte TaxAffectationTypeID,
        string TaxAffectationTypeName,
        byte RecordStateID
    );    
}