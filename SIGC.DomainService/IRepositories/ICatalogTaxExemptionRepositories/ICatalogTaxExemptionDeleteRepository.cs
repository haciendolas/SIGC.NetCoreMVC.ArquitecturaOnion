namespace SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories
{
    public interface ICatalogTaxExemptionDeleteRepository
    {
        Task<int> DeleteAsync(int CompanyID, int CatalogTaxExemptionID, CancellationToken CancellationToken = default);
    }
}
