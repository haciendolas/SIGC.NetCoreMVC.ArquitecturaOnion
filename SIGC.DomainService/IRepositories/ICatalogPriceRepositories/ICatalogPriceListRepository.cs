using SIGC.DomainModel.Dtos.CatalogPrice;

namespace SIGC.DomainService.IRepositories.ICatalogPriceRepositories
{
    public interface ICatalogPriceListRepository
    {
        Task<List<CatalogPriceListResponseDto>> ListAsync(int CompanyID,int CatalogID, CancellationToken CancellationToken = default);
    }
}