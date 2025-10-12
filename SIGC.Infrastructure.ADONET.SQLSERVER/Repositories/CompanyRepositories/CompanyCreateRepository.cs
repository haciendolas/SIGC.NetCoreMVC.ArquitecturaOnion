using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICompanyRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyCreateRepository : ICompanyCreateRepository
    {
        public Task<int> CreateAsync(Company Model, CancellationToken CancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
