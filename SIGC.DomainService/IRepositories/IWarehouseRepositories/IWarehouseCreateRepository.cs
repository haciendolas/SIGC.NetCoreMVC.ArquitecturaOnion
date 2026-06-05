using SIGC.DomainModel.Models;

namespace SIGC.DomainService.IRepositories.IWarehouseRepositories
{
    public interface IWarehouseCreateRepository
    {
        Task<string> CreateAsync(Warehouse Model, CancellationToken CancellationToken = default);
    }
}