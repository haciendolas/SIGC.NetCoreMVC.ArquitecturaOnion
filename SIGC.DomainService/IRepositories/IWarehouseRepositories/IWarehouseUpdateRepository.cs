using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IWarehouseRepositories
{
    public interface IWarehouseUpdateRepository
    {
        Task<string> UpdateAsync(Warehouse Model, CancellationToken CancellationToken = default);
    }
}