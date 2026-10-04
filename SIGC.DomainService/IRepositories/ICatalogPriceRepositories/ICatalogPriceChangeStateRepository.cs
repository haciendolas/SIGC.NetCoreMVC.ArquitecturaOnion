using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogPriceRepositories
{
    public interface ICatalogPriceChangeStateRepository
    {
        Task<int> ChangeStateAsync(CatalogPrice Model, CancellationToken CancellationToken = default);
    }
}