using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.ICatalogTherapeuticActionRepositories
{
    public interface ICatalogTherapeuticActionCreateUpdateRepository
    {
        Task<int> CreateUpdateAsync(CatalogTherapeuticAction Model, CancellationToken CancellationToken = default);
    }
}
