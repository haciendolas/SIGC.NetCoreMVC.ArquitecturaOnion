using SIGC.DomainModel.Dtos.CatalogPresentation;

namespace SIGC.DomainService.IRepositories.ICatalogPresentationRepositories
{
    public interface ICatalogPresentationListRepository
    {
        Task<List<CatalogPresentationListResponseDto>> ListAsync(int CompanyID,int CatalogID, CancellationToken CancellationToken = default);
    }
}