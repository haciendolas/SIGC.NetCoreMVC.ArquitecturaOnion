using SIGC.DomainModel.Dtos.Category;

namespace SIGC.DomainService.IRepositories.ICategoryRepositories
{
    public interface ICategoryListRepository
    {
        Task<List<CategoryListResponseDto>> ListAsync(int CompanyID, CancellationToken CancellationToken = default);
    }
}