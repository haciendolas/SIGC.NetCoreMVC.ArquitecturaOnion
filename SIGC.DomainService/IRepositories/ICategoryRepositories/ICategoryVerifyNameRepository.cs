using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICategoryRepositories
{
    public interface ICategoryVerifyNameRepository
    {
        Task<string> VerifyNameAsync(Category Model, CancellationToken CancellationToken = default);
    }
}