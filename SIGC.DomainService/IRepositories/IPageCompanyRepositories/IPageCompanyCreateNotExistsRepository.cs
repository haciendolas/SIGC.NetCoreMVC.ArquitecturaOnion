using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.IPageCompanyRepositories
{
    public interface IPageCompanyCreateNotExistsRepository
    {
        Task<int> CreateNotExistsAsync(PageCompany Model, CancellationToken CancellationToken = default);
    }
}