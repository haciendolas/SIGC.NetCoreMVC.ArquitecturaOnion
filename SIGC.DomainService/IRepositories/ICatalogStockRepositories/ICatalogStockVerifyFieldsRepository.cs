using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogStockRepositories
{
    public interface ICatalogStockVerifyFieldsRepository
    {
        Task<string> VerifyFieldsAsync(CatalogStock Model, CancellationToken CancellationToken = default);
    }
}