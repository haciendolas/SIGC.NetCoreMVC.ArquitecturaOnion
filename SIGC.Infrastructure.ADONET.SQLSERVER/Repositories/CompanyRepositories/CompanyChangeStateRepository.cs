using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICompanyRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CompanyRepositories
{
    internal class CompanyChangeStateRepository : ICompanyChangeStateRepository
    {
        public Task<int> ChangeStateAsync(Company Model, CancellationToken CancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
