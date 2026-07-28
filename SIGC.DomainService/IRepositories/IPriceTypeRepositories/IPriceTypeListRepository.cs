using SIGC.DomainModel.Dtos.PriceType;

namespace SIGC.DomainService.IRepositories.IPriceTypeRepositories
{
    public interface IPriceTypeListRepository
    {
        Task<List<PriceTypeListResponseDto>> ListAsync(CancellationToken CancellationToken = default);
    }
}