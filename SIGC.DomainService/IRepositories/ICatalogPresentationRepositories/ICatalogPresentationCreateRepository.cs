using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogPresentationRepositories
{
    public interface ICatalogPresentationCreateRepository
    {
        Task<int> CreateAsync(CatalogPresentation Model, CancellationToken CancellationToken = default);
    }
}