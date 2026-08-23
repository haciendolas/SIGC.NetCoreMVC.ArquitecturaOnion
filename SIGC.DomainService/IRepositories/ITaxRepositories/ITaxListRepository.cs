using SIGC.DomainModel.Dtos.Tax;

namespace SIGC.DomainService.IRepositories.ITaxRepositories
{
    public interface ITaxListRepository
    {
        Task<List<TaxListResponseDto>> ListAsync(int CountryID, CancellationToken CancellationToken = default);
    }
}