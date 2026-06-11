using SIGC.DomainModel.Dtos.Warehouse;

namespace SIGC.DomainService.IRepositories.IWarehouseRepositories
{
   public interface IWarehouseGetRepository
    {
        Task<WarehouseGetResponseDto?> GetAsync(int CompanyID, int WarehouseID, CancellationToken CancellationToken = default);
    }
}