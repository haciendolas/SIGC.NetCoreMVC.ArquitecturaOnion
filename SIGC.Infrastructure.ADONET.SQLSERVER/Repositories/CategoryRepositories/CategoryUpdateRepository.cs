using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
    internal class CategoryUpdateRepository : ICategoryUpdateRepository
    {
        public Task<int> UpdateAsync(Category Model, CancellationToken CancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}