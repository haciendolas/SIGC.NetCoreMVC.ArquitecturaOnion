using Cross.Library.Service.Enums;
using MediatR;
using SIGC.ApplicationService.Commons;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Dtos.User;
using SIGC.DomainService.IRepositories.IUserRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.AuthFeatures.Queries.UserLogin
{
    internal class UserLoginQueryHandler(
         IUserLoginRepository UserLoginRepository,
         IMessageService MessageService,
         IGenerateJWTToken GenerateJWTToken
    ) : IRequestHandler<UserLoginQueryRequest, MsgResponse<object>>{
        public async Task<MsgResponse<object>> Handle(UserLoginQueryRequest request, CancellationToken cancellationToken)
        {
            var UserLoginRequest = new UserLoginRequestDto()
            {
                CompanyDocumentNumber = request.CompanyDocumentNumber,
                UserName = request.UserName,
                UserPassword = request.UserPassword
            };
           
            var UserLoginResponse = await UserLoginRepository.GetAsync(UserLoginRequest);

            var MsgResponse = new MsgResponse<object>();
                MsgResponse.Type = MessageTypeConst.QUERY;

            if(UserLoginResponse == null){
                MsgResponse.Message = MessageDescriptionConst.INVALID_CREDENTIAL_DESCRIPTION;                
            }
            else{
                MsgResponse.Message = MessageDescriptionConst.VALID_CREDENTIAL_DESCRIPTION;
                AppUserDto AppUser = new AppUserDto(){
                    UserID = UserLoginResponse.Value.UserID,
                    UserName = request.UserName,
                    UserFirstName = UserLoginResponse.Value.UserFirstName,
                    UserLastName = UserLoginResponse.Value.UserLastName,
                    UserMail = UserLoginResponse.Value.UserMail,
                    CompanyID = UserLoginResponse.Value.CompanyID,
                    IdiomID = (short)IdiomEnum.Spanish,
                    CompanyDocumentNumber = UserLoginResponse.Value.CompanyDocumentNumber,
                    CompanyTradeName = UserLoginResponse.Value.CompanyTradeName,
                    CompanySocialReason = UserLoginResponse.Value.CompanySocialReason,
                    RoleCodes = "1,2"
                };
                var AccessToken = await GenerateJWTToken.GenerateJWTToken(AppUser);
                MsgResponse.Data = new { AccessToken = AccessToken };
            }
           return MsgResponse;
        }

        private List<UserResponseDto> UserList()
        {
            var list = new List<UserResponseDto>() {
               new UserResponseDto
               {
                   UserId=1,
                   CompanyId=1,
                   CompanyDocumentNumber="10404358087",
                   UserName="Administrador",
                   UserPassword="123456"
               },
               new UserResponseDto
               {
                   UserId=2,
                   CompanyId=1,
                   CompanyDocumentNumber="10404358086",
                   UserName="jcastillo",
                   UserPassword="123456"
               }
            };

            return list;
        }

    }
}
