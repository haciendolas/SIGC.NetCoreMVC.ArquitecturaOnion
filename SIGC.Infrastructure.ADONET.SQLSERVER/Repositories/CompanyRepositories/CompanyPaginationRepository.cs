using SIGC.DomainModel.Dtos.Company;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainService.IRepositories.ICompanyRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyPaginationRepository : ICompanyPaginationRepository
    {
        public Task<PaginationResponseDto<CompanyPaginationResponseDto>> PaginationAsync(CompanyPaginationResquestDto CompanyPaginationResquest, CancellationToken CancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
