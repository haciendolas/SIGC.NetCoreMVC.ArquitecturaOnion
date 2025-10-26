using SIGC.DomainModel.Dtos.Constant;

namespace SIGC.DomainService.IRepositories.IConstantRepositories
{
    public interface IConstantListRepository
    {
        Task<List<ConstantListResponseDto>> ListAsync(string ConstantClass, CancellationToken CancellationToken = default);
    }
}