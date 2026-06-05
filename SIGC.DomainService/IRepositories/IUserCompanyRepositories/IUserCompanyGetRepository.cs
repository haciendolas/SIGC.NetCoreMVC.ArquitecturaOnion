using SIGC.DomainModel.Dtos.UserCompany;

namespace SIGC.DomainService.IRepositories.IUserCompanyRepositories
{
    public interface IUserCompanyGetRepository
    {
        Task<UserCompanyGetResponseDto?> GetAsync(int UserID,int CompanyID, CancellationToken CancellationToken = default);
    }
}