using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories
{
    public interface ICatalogTaxExemptionCreateRepository
    {
        Task<int> CreateAsync(CatalogTaxExemption Model, CancellationToken CancellationToken = default);
    }
}
