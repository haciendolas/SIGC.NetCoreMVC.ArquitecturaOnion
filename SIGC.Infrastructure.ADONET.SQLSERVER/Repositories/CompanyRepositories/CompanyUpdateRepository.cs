using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICompanyRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyUpdateRepository : ICompanyUpdateRepository
    {
        public Task<int> UpdateAsync(Company Model, CancellationToken CancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
