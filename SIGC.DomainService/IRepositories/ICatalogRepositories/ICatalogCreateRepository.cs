using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogRepositories
{
    public interface ICatalogCreateRepository
    {
        Task<int> CreateAsync(Catalog Model, CancellationToken CancellationToken = default);
    }
}
