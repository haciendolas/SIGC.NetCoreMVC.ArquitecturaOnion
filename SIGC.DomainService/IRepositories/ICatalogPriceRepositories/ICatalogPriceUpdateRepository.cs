using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogPriceRepositories
{
    public interface ICatalogPriceUpdateRepository
    {
        Task<int> UpdateAsync(CatalogPrice Model, CancellationToken CancellationToken = default);
    }
}