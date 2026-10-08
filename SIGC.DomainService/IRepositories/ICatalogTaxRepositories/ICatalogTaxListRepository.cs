using SIGC.DomainModel.Dtos.CatalogTax;

namespace SIGC.DomainService.IRepositories.ICatalogTaxRepositories
{
    public interface ICatalogTaxListRepository
    {
        Task<List<CatalogTaxListResponseDto>> ListAsync(int CompanyID,int CatalogID, CancellationToken CancellationToken = default);
    }
}