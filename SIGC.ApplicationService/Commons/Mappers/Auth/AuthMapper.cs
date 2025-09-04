using Cross.Library.Service.Enums;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Dtos.Auth;

namespace SIGC.ApplicationService.Commons.Mappers.Auth
{
    public class AuthMapper : IAuthMapper
    {
        public AppUserDto AuthLoginResponseToAppUser(AuthLoginResponseDto AuthLoginResponse)
        {
            AppUserDto AppUser = new AppUserDto()
            {
                UserID = AuthLoginResponse.UserID,
                UserName = AuthLoginResponse.UserName,
                UserFirstName = AuthLoginResponse.UserFirstName,
                UserLastName = AuthLoginResponse.UserLastName,
                UserMail = AuthLoginResponse.UserMail,
                CompanyID = AuthLoginResponse.CompanyID,
                IdiomID = (short)IdiomEnum.Spanish,
                CompanyDocumentNumber = AuthLoginResponse.CompanyDocumentNumber,
                CompanyTradeName = AuthLoginResponse.CompanyTradeName,
                CompanySocialReason = AuthLoginResponse.CompanySocialReason,
                RoleCodes = "1,2"
            };
            return AppUser;
        }
    }
}