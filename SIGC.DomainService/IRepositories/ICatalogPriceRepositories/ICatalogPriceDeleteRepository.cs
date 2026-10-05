namespace SIGC.DomainService.IRepositories.ICatalogPriceRepositories
{
    public interface ICatalogPriceDeleteRepository
    {
        Task<int> DeleteAsync(int CompanyID, int CatalogPriceID, CancellationToken CancellationToken = default);
    }
}
