using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.ICatalogVariantValueRepositories
{
    public interface ICatalogVariantValueCreateUpdateRepository
    {
        Task<int> CreateUpdateAsync(CatalogVariantValue Model, CancellationToken CancellationToken = default);
    }
}