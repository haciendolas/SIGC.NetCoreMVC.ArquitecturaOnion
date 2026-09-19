using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogVariantRepositories
{
    public interface ICatalogVariantCreateRepository
    {
        Task<int> CreateAsync(CatalogVariant Model, CancellationToken CancellationToken = default);
    }
}
