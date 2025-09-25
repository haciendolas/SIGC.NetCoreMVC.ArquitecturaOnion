using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.IRolePermissionRepositories
{
    public interface IRolePermissionCreateRepository
    {
        Task<int> CreateAsync(RolePermission Model, CancellationToken CancellationToken = default);
    }
}