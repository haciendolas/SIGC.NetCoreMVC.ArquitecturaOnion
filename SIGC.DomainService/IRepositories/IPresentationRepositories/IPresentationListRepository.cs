using SIGC.DomainModel.Dtos.Presentation;

namespace SIGC.DomainService.IRepositories.IPresentationRepositories
{
    public interface IPresentationListRepository
    {
        Task<List<PresentationListResponseDto>> ListAsync(int CompanyID,int UnitMeasureID, CancellationToken CancellationToken = default);
    }
}