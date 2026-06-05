using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainModel.Dtos.User
{
    public record struct UserPaginationRequestDto
    (
        int CompanyID,
        string? UserFullName,     
        short StateID,
        PaginationParametersDto Parameters
    );
}