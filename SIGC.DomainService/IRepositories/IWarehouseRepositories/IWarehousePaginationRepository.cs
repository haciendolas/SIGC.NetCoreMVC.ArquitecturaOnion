using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainModel.Dtos.Warehouse;

namespace SIGC.DomainService.IRepositories.IWarehouseRepositories
{
    public interface IWarehousePaginationRepository
    {
        Task<PaginationResponseDto<WarehousePaginationResponseDto>> PaginationAsync(WarehousePaginationRequestDto WarehousePaginationRequest, CancellationToken CancellationToken = default);
    }
}