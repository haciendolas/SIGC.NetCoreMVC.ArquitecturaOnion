using SIGC.DomainModel.Dtos.Warehouse;

namespace SIGC.DomainService.IRepositories.IWarehouseRepositories
{
    public interface IWarehouseListRepository
    {
        Task<List<WarehouseListResponseDto>> ListAsync(int CompanyID,int EstablishmentID, CancellationToken CancellationToken = default);
    }
}