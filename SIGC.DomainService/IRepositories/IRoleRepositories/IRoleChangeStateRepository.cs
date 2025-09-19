using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IRoleRepositories
{
    public interface IRoleChangeStateRepository
    {
        Task<int> ChangeStateAsync(Role Model, CancellationToken CancellationToken = default);
    }
}