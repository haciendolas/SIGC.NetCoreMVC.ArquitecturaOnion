using SIGC.DomainModel.Dtos.Establishment;

namespace SIGC.DomainService.IRepositories.IEstablishmentRepositories
{
    public interface IEstablishmentListRepository
    {
        Task<List<EstablishmentListResponseDto>> ListAsync(int CompanyID,int PersonID, CancellationToken CancellationToken = default);
    }
}
