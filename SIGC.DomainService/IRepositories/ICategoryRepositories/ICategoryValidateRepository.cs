using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ICategoryRepositories
{
    public interface ICategoryValidateRepository
    {
        Task<string> ValidateAsync(Category Model, CancellationToken CancellationToken);
    }
}