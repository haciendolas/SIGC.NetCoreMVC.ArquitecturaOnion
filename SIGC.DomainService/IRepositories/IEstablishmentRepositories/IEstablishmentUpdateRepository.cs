using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IEstablishmentRepositories
{
    public interface IEstablishmentUpdateRepository
    {
        Task<string> UpdateAsync(Establishment Model, CancellationToken CancellationToken = default);
    }
}