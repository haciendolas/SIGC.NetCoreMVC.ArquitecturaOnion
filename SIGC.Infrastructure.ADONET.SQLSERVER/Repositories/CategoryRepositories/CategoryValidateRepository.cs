using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;

namespace SIGC.Infrastructure.ADONET.SQLSERVER.Repositories.CategoryRepositories
{
    internal class CategoryValidateRepository : ICategoryValidateRepository
    {
        public Task<string> ValidateAsync(Category Model, CancellationToken CancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}