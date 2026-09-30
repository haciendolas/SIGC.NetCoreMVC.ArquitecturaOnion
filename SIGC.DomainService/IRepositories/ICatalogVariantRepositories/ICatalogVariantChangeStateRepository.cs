using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogVariantRepositories
{
    public interface ICatalogVariantChangeStateRepository
    {
        Task<int> ChangeStateAsync(CatalogVariant Model, CancellationToken CancellationToken = default);
    }
}