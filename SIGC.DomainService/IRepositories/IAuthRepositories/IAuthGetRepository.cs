using SIGC.DomainModel.Dtos.Auth;

namespace SIGC.DomainService.IRepositories.IAuthRepositories
{
   public interface IAuthGetRepository
    {
        Task<AuthLoginResponseDto?> GetAsync(int UserID,int CompanyID,CancellationToken CancellationToken = default);
    }
}
