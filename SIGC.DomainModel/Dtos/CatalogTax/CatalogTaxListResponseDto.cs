namespace SIGC.DomainModel.Dtos.CatalogTax
{
    public sealed record CatalogTaxListResponseDto
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