using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogTaxRepositories
{
    public interface ICatalogTaxChangeStateRepository
    {
        Task<int> ChangeStateAsync(CatalogTax Model, CancellationToken CancellationToken = default);
    }
}