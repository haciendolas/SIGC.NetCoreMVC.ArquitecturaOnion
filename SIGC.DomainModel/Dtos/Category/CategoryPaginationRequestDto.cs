using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainModel.Dtos.Category
{
    public sealed record CategoryPaginationRequestDto
    (
        int CompanyID,
        byte? RecordStateID,
        PaginationParametersDto Parameters
    );
}