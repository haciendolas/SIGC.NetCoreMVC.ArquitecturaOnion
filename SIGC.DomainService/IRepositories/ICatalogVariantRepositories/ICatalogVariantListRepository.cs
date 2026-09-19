using SIGC.DomainModel.Dtos.CatalogVariant;

namespace SIGC.DomainService.IRepositories.ICatalogVariantRepositories
{
    public interface ICatalogVariantListRepository
    {
        Task<List<CatalogVariantListResponseDto>> ListAsync(int CompanyID,int CatalogID, CancellationToken CancellationToken = default);
    }
}