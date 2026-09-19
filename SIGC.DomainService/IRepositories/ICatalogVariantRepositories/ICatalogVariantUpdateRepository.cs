using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogVariantRepositories
{
    public interface ICatalogVariantUpdateRepository
    {
        Task<int> UpdateAsync(CatalogVariant Model, CancellationToken CancellationToken = default);
    }
}
