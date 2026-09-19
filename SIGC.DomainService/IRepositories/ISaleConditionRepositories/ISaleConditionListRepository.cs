using SIGC.DomainModel.Dtos.SaleCondition;

namespace SIGC.DomainService.IRepositories.ISaleConditionRepositories
{
    public interface ISaleConditionListRepository
    {
        Task<List<SaleConditionListResponseDto>> ListAsync(CancellationToken CancellationToken = default);
    }
}