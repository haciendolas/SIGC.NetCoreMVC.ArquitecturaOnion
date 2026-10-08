using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogTaxRepositories
{
    public interface ICatalogTaxCreateRepository
    {
        Task<int> CreateAsync(CatalogTax Model, CancellationToken CancellationToken = default);
    }
}
