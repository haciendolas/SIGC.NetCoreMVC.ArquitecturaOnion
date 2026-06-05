using SIGC.DomainModel.Dtos.Category;
using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainService.IRepositories.ICategoryRepositories
{
    public interface ICategoryPaginationRepository
    {
        Task<PaginationResponseDto<CategoryPaginationResponseDto>> PaginationAsync(CategoryPaginationRequestDto CategoryPaginationRequest, CancellationToken CancellationToken = default);
    }
}