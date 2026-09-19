using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogPresentationRepositories
{
    public interface ICatalogPresentationUpdateRepository
    {
        Task<int> UpdateAsync(CatalogPresentation Model, CancellationToken CancellationToken = default);
    }
}
