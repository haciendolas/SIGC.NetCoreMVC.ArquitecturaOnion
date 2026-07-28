using SIGC.DomainModel.Dtos.TherapeuticAction;

namespace SIGC.DomainService.IRepositories.ITherapeuticActionRepositories
{
    public interface ITherapeuticActionListRepository
    {
        Task<List<TherapeuticActionListResponseDto>> ListAsync(CancellationToken CancellationToken = default);
    }
}