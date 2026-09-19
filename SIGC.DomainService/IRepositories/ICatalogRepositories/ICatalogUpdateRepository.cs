using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogRepositories
{
    public interface ICatalogUpdateRepository
    {
        Task<int> UpdateAsync(Catalog Model, CancellationToken CancellationToken = default);
    }
}
