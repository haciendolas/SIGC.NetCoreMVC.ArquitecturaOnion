using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IUserRepositories
{
    public interface IUserUpdateRepository
    {
        Task<int> UpdateAsync(User Model, CancellationToken CancellationToken = default);
    }
}
