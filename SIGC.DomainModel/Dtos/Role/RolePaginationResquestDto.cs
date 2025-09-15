using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainModel.Dtos.Role
{
    public record struct RolePaginationResquestDto
    (
        int CompanyID, 
        short StateID,      
        PaginationParametersDto Parameters
    );    
}