using SIGC.DomainModel.Dtos.CatalogTaxExemption;

namespace SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories
{
    public interface ICatalogTaxExemptionListRepository
    {
        Task<List<CatalogTaxExemptionListResponseDto>> ListAsync(int CompanyID,int CatalogID, CancellationToken CancellationToken = default);
    }
}