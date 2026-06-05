using SIGC.DomainModel.ValueObjects;

namespace SIGC.DomainService.IRepositories.IUserCompanyRepositories
{
    public interface IUserCompanyChangeStateRepository
    {
        Task<int> ChangeStateAsync(UserCompany Model, CancellationToken CancellationToken = default);
    }
}