using SIGC.DomainModel.Dtos.Brand;

namespace SIGC.DomainService.IRepositories.IBrandRepositories
{
    public interface IBrandListRepository
    {
        Task<List<BrandListResponseDto>> ListAsync(CancellationToken CancellationToken = default);
    }
}