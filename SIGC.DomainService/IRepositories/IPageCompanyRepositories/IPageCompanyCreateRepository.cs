using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.IPageCompanyRepositories
{
    public interface IPageCompanyCreateRepository
    {
        Task<int> CreateAsync(PageCompany Model, CancellationToken CancellationToken = default);
    }
}