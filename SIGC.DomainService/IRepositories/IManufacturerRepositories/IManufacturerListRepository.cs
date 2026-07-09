using SIGC.DomainModel.Dtos.Manufacturer;

namespace SIGC.DomainService.IRepositories.IManufacturerRepositories
{
    public interface IManufacturerListRepository
    {
        Task<List<ManufacturerListResponseDto>> ListAsync(CancellationToken CancellationToken = default);
    }
}