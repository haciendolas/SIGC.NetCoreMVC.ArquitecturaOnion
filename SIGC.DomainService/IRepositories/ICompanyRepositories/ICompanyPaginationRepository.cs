using SIGC.DomainModel.Dtos.Company;
using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainService.IRepositories.ICompanyRepositories
{
    public interface ICompanyPaginationRepository
    {
        Task<PaginationResponseDto<CompanyPaginationResponseDto>> PaginationAsync(CompanyPaginationRequestDto CompanyPaginationResquest, CancellationToken CancellationToken = default);
    }
}