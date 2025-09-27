
using SIGC.DomainModel.Dtos.PageCompany;

namespace SIGC.DomainService.IRepositories.IPageCompanyRepositories
{
    public interface IPageCompanyListRepository
    {
        Task<List<PageCompanyListResponseDto>> ListAsync(int CompanyID, CancellationToken CancellationToken = default);
    }
}