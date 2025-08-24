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

            if(UserLoginResponse == default){
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.INVALID_CREDENTIAL);                
            }
            else{
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.VALID_CREDENTIAL);
                AppUserDto AppUser = new AppUserDto(){
                    UserID = UserLoginResponse.UserID,
                    UserName = request.UserName,
                    UserFirstName = UserLoginResponse.UserFirstName,
                    UserLastName = UserLoginResponse.UserLastName,
                    UserMail = UserLoginResponse.UserMail,
                    CompanyID = UserLoginResponse.CompanyID,
                    IdiomID = (short)IdiomEnum.Spanish,
                    CompanyDocumentNumber = UserLoginResponse.CompanyDocumentNumber,
                    CompanyTradeName = UserLoginResponse.CompanyTradeName,
                    CompanySocialReason = UserLoginResponse.CompanySocialReason,
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
