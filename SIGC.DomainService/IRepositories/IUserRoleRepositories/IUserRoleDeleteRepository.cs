namespace SIGC.DomainService.IRepositories.IUserRoleRepositories
{
    public interface IUserRoleDeleteRepository
    {
        Task<int> DeleteAsync(int CompanyID, int UserID, CancellationToken CancellationToken = default);
    }
}
