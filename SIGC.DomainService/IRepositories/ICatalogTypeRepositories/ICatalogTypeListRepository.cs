using SIGC.DomainModel.Dtos.CatalogType;

namespace SIGC.DomainService.IRepositories.ICatalogTypeRepositories
{
    public interface ICatalogTypeListRepository
    {
        Task<List<CatalogTypeListResponseDto>> ListAsync(CancellationToken CancellationToken = default);
    }
}