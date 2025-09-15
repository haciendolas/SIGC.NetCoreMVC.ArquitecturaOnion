using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainModel.Dtos.Role;

namespace SIGC.DomainService.IRepositories.IRoleRepositories
{
    public interface IRolePaginationRepository
    {
        Task<PaginationResponseDto<RolePaginationResponseDto>> PaginationAsync(RolePaginationResquestDto RolePaginationResquest, CancellationToken CancellationToken = default);
    }
}