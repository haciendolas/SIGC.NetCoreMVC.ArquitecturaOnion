using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogPriceRepositories
{
    public interface ICatalogPriceVerifyFieldsRepository
    {
        Task<string> VerifyFieldsAsync(CatalogPrice Model, CancellationToken CancellationToken = default);
    }
}