using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogTaxRepositories
{
    public interface ICatalogTaxUpdateRepository
    {
        Task<int> UpdateAsync(CatalogTax Model, CancellationToken CancellationToken = default);
    }
}