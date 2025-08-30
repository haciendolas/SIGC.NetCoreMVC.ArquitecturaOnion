using SIGC.DomainModel.Dtos.Category;
using SIGC.DomainService.IRepositories.ICategoryRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
    internal class CategoryGetRepository : ICategoryGetRepository
    {
        public Task<CategoryGetResponseDto?> GetAsync(int CategoryId, CancellationToken CancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}