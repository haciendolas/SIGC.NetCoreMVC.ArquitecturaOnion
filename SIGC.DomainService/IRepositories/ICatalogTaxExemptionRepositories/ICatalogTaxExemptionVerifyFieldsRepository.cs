using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories
{
    public interface ICatalogTaxExemptionVerifyFieldsRepository
    {
        Task<string> VerifyFieldsAsync(CatalogTaxExemption Model, CancellationToken CancellationToken = default);
    }
}