namespace SIGC.DomainService.IRepositories.ICatalogActiveIngredientRepositories
{
    public interface ICatalogActiveIngredientDeleteRepository
    {
        Task<int> DeleteAsync(int CompanyID, int CatalogID,List<int> ActiveIngredientIDList, CancellationToken CancellationToken = default);
    }
}
