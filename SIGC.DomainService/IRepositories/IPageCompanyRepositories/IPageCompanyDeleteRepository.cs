namespace SIGC.DomainService.IRepositories.IPageCompanyRepositories
{
    public interface IPageCompanyDeleteRepository
    {
        Task<int> DeleteAsync(int CompanyID, CancellationToken CancellationToken = default);
    }
}