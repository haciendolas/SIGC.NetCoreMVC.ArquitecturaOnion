using SIGC.DomainModel.Dtos.Catalog;
using SIGC.DomainModel.Dtos.Pagination;

namespace SIGC.DomainService.IRepositories.ICatalogRepositories
{
    public interface ICatalogPaginationRepository
    {
        Task<PaginationResponseDto<CatalogPaginationResponseDto>> PaginationAsync(CatalogPaginationRequestDto CatalogPaginationRequest, CancellationToken CancellationToken = default);
    }
}