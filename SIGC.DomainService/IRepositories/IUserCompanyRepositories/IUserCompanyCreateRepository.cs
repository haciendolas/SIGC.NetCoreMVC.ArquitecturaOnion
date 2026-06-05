using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.IUserCompanyRepositories
{
    public interface IUserCompanyCreateRepository
    {
        Task<int> CreateAsync(UserCompany Model, CancellationToken CancellationToken = default);
    }
}