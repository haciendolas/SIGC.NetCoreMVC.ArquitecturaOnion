using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainModel.Dtos.Company
{
    public record struct CompanyPaginationRequestDto
    (
        int CompanyIDRegister,                 
        short? TaxpayerTypeID, 
        short? RubroID,
        string? CompanyDocumentNumber, 
        string? CompanySocialReason,
        short StateID,
        PaginationParametersDto Parameters
    );
}