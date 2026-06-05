using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IUserRepositories
{
    public interface IUserVerifyNameAndMailRepository
    {
        Task<string> VerifyNameAndMailAsync(User Model, CancellationToken CancellationToken = default);
    }
}