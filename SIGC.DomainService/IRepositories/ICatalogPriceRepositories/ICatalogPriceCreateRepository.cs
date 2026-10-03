using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogPriceRepositories
{
    public interface ICatalogPriceCreateRepository
    {
        Task<int> CreateAsync(CatalogPrice Model, CancellationToken CancellationToken = default);
    }
}
