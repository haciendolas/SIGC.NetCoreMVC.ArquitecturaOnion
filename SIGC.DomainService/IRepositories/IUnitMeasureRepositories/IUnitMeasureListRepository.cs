using SIGC.DomainModel.Dtos.UnitMeasure;

namespace SIGC.DomainService.IRepositories.IUnitMeasureRepositories
{
    public interface IUnitMeasureListRepository
    {
        Task<List<UnitMeasureListResponseDto>> ListAsync(int CountryID, CancellationToken CancellationToken = default);
    }
}