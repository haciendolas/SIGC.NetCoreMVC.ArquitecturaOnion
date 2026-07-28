using SIGC.DomainModel.Dtos.PrescriptionType;

namespace SIGC.DomainService.IRepositories.IPrescriptionTypeRepositories
{
    public interface IPrescriptionTypeListRepository
    {
        Task<List<PrescriptionTypeListResponseDto>> ListAsync(CancellationToken CancellationToken = default);
    }
}