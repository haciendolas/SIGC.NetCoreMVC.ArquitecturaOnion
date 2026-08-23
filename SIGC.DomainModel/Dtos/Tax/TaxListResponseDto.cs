namespace SIGC.DomainModel.Dtos.Tax
{
    public sealed record TaxListResponseDto
    (
        short TaxID,
        string TaxCode,
        string TaxName,
        decimal TaxValor
    );
}