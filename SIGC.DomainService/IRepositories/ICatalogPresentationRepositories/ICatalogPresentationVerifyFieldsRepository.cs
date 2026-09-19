using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogPresentationRepositories
{
    public interface ICatalogPresentationVerifyFieldsRepository
    {
        Task<string> VerifyFieldsAsync(CatalogPresentation Model, CancellationToken CancellationToken = default);
    }
}