using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IEstablishmentRepositories
{
    public interface IEstablishmentCreateRepository
    {
        Task<string> CreateAsync(Establishment Model, CancellationToken CancellationToken = default);
    }
}