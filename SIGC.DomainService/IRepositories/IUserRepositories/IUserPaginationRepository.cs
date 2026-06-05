using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainModel.Dtos.User;

namespace SIGC.DomainService.IRepositories.IUserRepositories
{
    public interface IUserPaginationRepository
    {
        Task<PaginationResponseDto<UserPaginationResponseDto>> PaginationAsync(UserPaginationRequestDto UserPaginationRequest, CancellationToken CancellationToken = default);
    }
}