namespace SIGC.DomainService.IRepositories.IRolePermissionRepositories
{
    public interface IRolePermissionDeleteRepository
    {
        Task<int> DeleteAsync(int RoleID, CancellationToken CancellationToken = default);
    }
}