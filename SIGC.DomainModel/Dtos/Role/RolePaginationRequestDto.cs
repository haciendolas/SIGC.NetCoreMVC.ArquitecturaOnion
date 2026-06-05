using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainModel.Dtos.Role
{
    public record struct RolePaginationRequestDto
    (
        int CompanyID, 
        short StateID,      
        PaginationParametersDto Parameters
    );    
}