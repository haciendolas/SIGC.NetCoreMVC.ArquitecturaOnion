using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICategoryRepositories
{
    public interface ICategoryUpdateRepository
    {
        Task<int> UpdateAsync(Category Model, CancellationToken CancellationToken);
    }
}
