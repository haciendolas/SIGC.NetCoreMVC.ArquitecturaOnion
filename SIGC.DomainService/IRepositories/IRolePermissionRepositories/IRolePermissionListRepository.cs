using SIGC.DomainModel.Dtos.RolePermission;

namespace SIGC.DomainService.IRepositories.IRolePermissionRepositories
{
    public interface IRolePermissionListRepository
    {
        Task<List<RolePermissionListResponseDto>> ListAsync(int UserID, int CompanyID, CancellationToken CancellationToken = default);
    }
}