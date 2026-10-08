using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogTaxRepositories
{
    public interface ICatalogTaxVerifyFieldsRepository
    {
        Task<string> VerifyFieldsAsync(CatalogTax Model, CancellationToken CancellationToken = default);
    }
}