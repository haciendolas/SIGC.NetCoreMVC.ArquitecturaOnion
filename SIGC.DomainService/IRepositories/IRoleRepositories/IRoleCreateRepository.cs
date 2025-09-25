using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IRoleRepositories
{
    public interface IRoleCreateRepository
    {
        Task<int> CreateAsync(Role Model, CancellationToken CancellationToken = default);
    }
}