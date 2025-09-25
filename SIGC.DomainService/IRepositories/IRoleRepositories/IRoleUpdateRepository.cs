using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IRoleRepositories
{
    public interface IRoleUpdateRepository
    {
        Task<int> UpdateAsync(Role Model, CancellationToken CancellationToken = default);
    }
}