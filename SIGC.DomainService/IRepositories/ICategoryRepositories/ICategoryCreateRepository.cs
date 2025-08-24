using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICategoryRepositories
{
    public interface ICategoryCreateRepository
    {
        Task<int> CreateAsync(Category Model, CancellationToken CancellationToken);
    }
}
