using SIGC.DomainModel.Dtos.Company;
using SIGC.DomainService.IRepositories.ICompanyRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyGetRepository : ICompanyGetRepository
    {
        public Task<CompanyGetResponseDto?> GetAsync(int CompanyID, CancellationToken CancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
