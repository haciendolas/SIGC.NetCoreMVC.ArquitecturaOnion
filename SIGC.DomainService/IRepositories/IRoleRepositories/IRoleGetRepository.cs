using SIGC.DomainModel.Dtos.Role;

namespace SIGC.DomainService.IRepositories.IRoleRepositories
{
    public interface IRoleGetRepository
    {
        Task<RoleGetResponseDto?> GetAsync(int RoleID, CancellationToken CancellationToken = default);
    }
}