using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.ITokenRepositories
{
   public interface ITokenUpdateRevocationRepository
    {
        Task<int> UpdateRevocationAsync(Token Model, CancellationToken CancellationToken = default);
   }
}