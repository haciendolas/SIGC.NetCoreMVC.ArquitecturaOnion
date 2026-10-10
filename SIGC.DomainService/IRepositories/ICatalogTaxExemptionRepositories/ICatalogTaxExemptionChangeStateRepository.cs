using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories
{
    public interface ICatalogTaxExemptionChangeStateRepository
    {
        Task<int> ChangeStateAsync(CatalogTaxExemption Model, CancellationToken CancellationToken = default);
    }
}