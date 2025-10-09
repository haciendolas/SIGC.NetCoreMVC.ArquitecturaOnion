namespace SIGC.DomainModel.Dtos.Company
{
    public record struct CompanyListResponseDto    
    (
        int CompanyID,
        string CompanyDocumentNumber,
        string CompanySocialReason
    );
}