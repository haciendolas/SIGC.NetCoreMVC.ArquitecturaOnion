namespace SIGC.DomainModel.Dtos.CatalogTaxExemption
{
    public sealed record CatalogTaxExemptionListResponseDto
    (
        int CatalogTaxExemptionID,
        int CatalogTaxID,
        string EstablishmentName,
        byte RecordStateID
    );    
}