using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IEstablishmentRepositories
{
    public interface IEstablishmentChangeStateRepository
    {
        Task<int> ChangeStateAsync(Establishment Model, CancellationToken CancellationToken = default);
    }
}