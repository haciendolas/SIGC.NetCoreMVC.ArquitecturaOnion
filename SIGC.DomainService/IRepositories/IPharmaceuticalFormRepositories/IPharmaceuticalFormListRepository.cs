using SIGC.DomainModel.Dtos.PharmaceuticalForm;

namespace SIGC.DomainService.IRepositories.IPharmaceuticalFormRepositories
{
    public interface IPharmaceuticalFormListRepository
    {
        Task<List<PharmaceuticalFormListResponseDto>> ListAsync(CancellationToken CancellationToken = default);
    }
}