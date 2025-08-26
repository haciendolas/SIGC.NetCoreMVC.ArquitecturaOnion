using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICategoryRepositories
{
    public interface ICategoryChangeStateRepository
    {
        Task<int> ChangeStateAsync(Category Model, CancellationToken CancellationToken);
    }
}