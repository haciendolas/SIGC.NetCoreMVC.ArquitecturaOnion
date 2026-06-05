using SIGC.DomainModel.Dtos.Establishment;

namespace SIGC.DomainService.IRepositories.IEstablishmentRepositories
{
   public interface IEstablishmentGetRepository
    {
        Task<EstablishmentGetResponseDto?> GetAsync(int CompanyID, int EstablishmentID, CancellationToken CancellationToken = default);
    }
}