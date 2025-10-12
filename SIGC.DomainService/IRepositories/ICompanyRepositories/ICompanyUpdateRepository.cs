using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICompanyRepositories
{
    public interface ICompanyUpdateRepository
    {
        Task<int> UpdateAsync(Company Model, CancellationToken CancellationToken = default);
    }
}