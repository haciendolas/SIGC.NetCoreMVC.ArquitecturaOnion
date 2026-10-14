using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogStockRepositories
{
    public interface ICatalogStockCreateRepository
    {
        Task<int> CreateAsync(CatalogStock Model, CancellationToken CancellationToken = default);
    }
}
