using SIGC.DomainModel.Dtos.Auth;

namespace SIGC.DomainService.IRepositories.IAuthRepositories
{
   public interface IAuthLoginRepository{
        Task<AuthLoginResponseDto?> LoginAsync(AuthLoginRequestDto UserCredentials, CancellationToken CancellationToken = default);
   }
}