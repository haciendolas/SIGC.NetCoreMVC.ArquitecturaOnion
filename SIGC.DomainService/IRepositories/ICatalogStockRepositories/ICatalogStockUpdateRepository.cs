using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogStockRepositories
{
    public interface ICatalogStockUpdateRepository
    {
        Task<int> UpdateAsync(CatalogStock Model, CancellationToken CancellationToken = default);
    }
}