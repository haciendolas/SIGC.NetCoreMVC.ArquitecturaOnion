using SIGC.DomainModel.Dtos.Page;

namespace SIGC.DomainService.IRepositories.IPageRepositories
{
    public interface IPageListRepository
    {
        Task<List<PageListResponseDto>> ListAsync(CancellationToken CancellationToken = default);
    }
}