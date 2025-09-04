using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Dtos.Auth;

namespace SIGC.ApplicationService.Commons.Mappers.Auth
{
    public interface IAuthMapper
    {
        AppUserDto AuthLoginResponseToAppUser(AuthLoginResponseDto AuthLoginResponse);        
    }
}
