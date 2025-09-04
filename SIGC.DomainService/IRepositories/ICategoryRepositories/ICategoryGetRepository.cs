using SIGC.DomainModel.Dtos.Category;

namespace SIGC.DomainService.IRepositories.ICategoryRepositories
{
   public interface ICategoryGetRepository
    {
        Task<CategoryGetResponseDto?> GetAsync(int CategoryId, CancellationToken CancellationToken = default);
    }
}