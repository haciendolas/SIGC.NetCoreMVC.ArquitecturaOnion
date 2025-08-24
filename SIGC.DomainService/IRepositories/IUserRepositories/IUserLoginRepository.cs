using SIGC.DomainModel.Dtos.User;

namespace SIGC.DomainService.IRepositories.IUserRepositories
{
   public interface IUserLoginRepository
    {
        Task<UserLoginResponseDto> GetAsync(UserLoginRequestDto UserCredentials);
    }
}
