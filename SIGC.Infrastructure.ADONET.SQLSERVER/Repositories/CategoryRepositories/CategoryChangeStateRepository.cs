using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
   internal class CategoryChangeStateRepository : ICategoryChangeStateRepository
    {
        public Task<int> ChangeStateAsync(Category Model, CancellationToken CancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}