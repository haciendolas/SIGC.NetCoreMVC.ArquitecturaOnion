using SIGC.DomainModel.Dtos.Role;

namespace SIGC.DomainService.IRepositories.IRoleRepositories
{
    public interface IRoleListRepository
    {
        Task<List<RoleListResponseDto>> ListAsync(int CompanyID, CancellationToken CancellationToken = default);
    }
}