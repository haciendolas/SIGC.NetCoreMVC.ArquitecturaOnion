using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainModel.Dtos.Company
{
    public record struct CompanyPaginationResquestDto
    (
        int CompanyID, 
        short StateID,      
        PaginationParametersDto Parameters
    );    
}