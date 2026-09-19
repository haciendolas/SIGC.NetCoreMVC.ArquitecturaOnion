namespace SIGC.DomainService.IRepositories.ICatalogVariantValueRepositories
{
    public interface ICatalogVariantValueDeleteRepository
    {
        Task<int> DeleteAsync(int CompanyID, int CatalogVariantID, List<short> AttributeValueIDList, CancellationToken CancellationToken = default);
    }
}
