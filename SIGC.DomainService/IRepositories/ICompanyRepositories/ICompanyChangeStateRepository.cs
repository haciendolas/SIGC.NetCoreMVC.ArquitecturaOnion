using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICompanyRepositories
{
    public interface ICompanyChangeStateRepository
    {
        Task<int> ChangeStateAsync(Company Model, CancellationToken CancellationToken = default);
    }
}