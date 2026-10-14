using SIGC.DomainModel.Dtos.CatalogStock;

namespace SIGC.DomainService.IRepositories.ICatalogStockRepositories
{
    public interface ICatalogStockListRepository
    {
        Task<List<CatalogStockListResponseDto>> ListAsync(int CompanyID,int CatalogID,int? EstablishmentID, CancellationToken CancellationToken = default);
    }
}