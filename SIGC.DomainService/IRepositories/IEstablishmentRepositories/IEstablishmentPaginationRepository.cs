using SIGC.DomainModel.Dtos.Establishment;
using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainService.IRepositories.IEstablishmentRepositories
{
    public interface IEstablishmentPaginationRepository
    {
        Task<PaginationResponseDto<EstablishmentPaginationResponseDto>> PaginationAsync(EstablishmentPaginationRequestDto EstablishmentPaginationRequest, CancellationToken CancellationToken = default);
    }
}