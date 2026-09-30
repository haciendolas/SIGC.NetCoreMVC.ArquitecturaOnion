using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogPresentationRepositories
{
    public interface ICatalogPresentationChangeStateRepository
    {
        Task<int> ChangeStateAsync(CatalogPresentation Model, CancellationToken CancellationToken = default);
    }
}