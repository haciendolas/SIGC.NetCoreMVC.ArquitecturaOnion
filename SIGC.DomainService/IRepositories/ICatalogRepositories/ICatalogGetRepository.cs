using SIGC.DomainModel.Dtos.Catalog;

namespace SIGC.DomainService.IRepositories.ICatalogRepositories
{
   public interface ICatalogGetRepository
    {
        Task<CatalogGetResponseDto?> GetAsync(int CompanyID, int CatalogID, CancellationToken CancellationToken = default);
    }
}