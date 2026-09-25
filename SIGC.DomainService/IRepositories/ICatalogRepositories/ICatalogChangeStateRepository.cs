using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogRepositories
{
    public interface ICatalogChangeStateRepository
    {
        Task<int> ChangeStateAsync(Catalog Model, CancellationToken CancellationToken = default);
    }
}