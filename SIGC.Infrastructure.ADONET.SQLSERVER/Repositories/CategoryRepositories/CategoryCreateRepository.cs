using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
   internal class CategoryCreateRepository : ICategoryCreateRepository
    {
        public Task<int> CreateAsync(Category Model, CancellationToken CancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}