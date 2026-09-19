namespace SIGC.DomainService.IRepositories.ICatalogTherapeuticActionRepositories
{
    public interface ICatalogTherapeuticActionDeleteRepository
    {
        Task<int> DeleteAsync(int CompanyID, int CatalogID,List<short> TherapeuticActionIDList, CancellationToken CancellationToken = default);
    }
}
