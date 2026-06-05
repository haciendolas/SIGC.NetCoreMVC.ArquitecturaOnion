using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.IUserRoleRepositories
{
    public interface IUserRoleCreateRepository
    {
        Task<int> CreateAsync(UserRole Model, CancellationToken CancellationToken = default);
    }
}