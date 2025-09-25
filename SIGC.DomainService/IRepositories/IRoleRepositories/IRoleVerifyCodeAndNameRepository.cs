using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IRoleRepositories
{
    public interface IRoleVerifyCodeAndNameRepository
    {
        Task<string> VerifyCodeAndNameAsync(Role Model, CancellationToken CancellationToken = default);
    }
}