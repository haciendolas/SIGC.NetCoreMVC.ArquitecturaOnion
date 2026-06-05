using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.IUserCompanyRepositories
{
    public interface IUserCompanyUpdateRepository
    {
        Task<int> UpdateAsync(UserCompany Model, CancellationToken CancellationToken = default);
    }
}