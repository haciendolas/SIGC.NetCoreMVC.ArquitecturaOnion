using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICatalogVariantRepositories
{
    public interface ICatalogVariantVerifySkuAndNameRepository
    {
        Task<string> VerifySkuAndNameAsync(CatalogVariant Model, CancellationToken CancellationToken = default);
    }
}