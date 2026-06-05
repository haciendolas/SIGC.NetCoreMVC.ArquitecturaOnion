using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IUserRepositories
{
    public interface IUserCreateRepository
    {
        Task<int> CreateAsync(User Model, CancellationToken CancellationToken = default);
    }
}