using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IWarehouseRepositories
{
    public interface IWarehouseChangeStateRepository
    {
        Task<int> ChangeStateAsync(Warehouse Model, CancellationToken CancellationToken = default);
    }
}