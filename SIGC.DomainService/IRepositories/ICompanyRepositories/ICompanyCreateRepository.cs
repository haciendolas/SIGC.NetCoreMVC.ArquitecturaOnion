using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICompanyRepositories
{
    public interface ICompanyCreateRepository
    {
        Task<int> CreateAsync(Company Model, CancellationToken CancellationToken = default);
    }
}