using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ITokenRepositories
{
   public interface ITokenCreateRepository
   {
        Task<int> CreateAsync(Token Model, CancellationToken CancellationToken = default);
   }
}