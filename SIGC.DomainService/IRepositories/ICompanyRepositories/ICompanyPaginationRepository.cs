using SIGC.DomainModel.Dtos.Company;
using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainService.IRepositories.ICompanyRepositories
{
    public interface ICompanyPaginationRepository
    {
        Task<PaginationResponseDto<CompanyPaginationResponseDto>> PaginationAsync(CompanyPaginationResquestDto CompanyPaginationResquest, CancellationToken CancellationToken = default);
    }
}