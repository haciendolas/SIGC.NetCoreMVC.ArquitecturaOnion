using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.ApplicationService.Commons.Mappers.Auth;
using SIGC.DomainModel.Dtos;
using SIGC.DomainModel.Dtos.Auth;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IAuthRepositories;
using SIGC.DomainService.IRepositories.ITokenRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.AuthFeatures.Queries.AuthLoginToken
{
    internal class AuthLoginTokenQueryHandler(
         IAuthLoginRepository AuthLoginRepository,        
         IGenerateTokenService GenerateTokenService,
         ITokenCreateRepository TokenCreateRepository,
         IAuthMapper AuthMapper
    ) : IRequestHandler<AuthLoginTokenQueryRequest, MsgResponse<AuthTokenResponseDto?>>{
        public async Task<MsgResponse<AuthTokenResponseDto?>> Handle(AuthLoginTokenQueryRequest Request, CancellationToken CancellationToken)
        {
            var UserLoginRequest = new AuthLoginRequestDto()
            {
                CompanyDocumentNumber = Request.CompanyDocumentNumber,
                UserName = Request.UserName,
                UserPassword = Request.UserPassword
            };
           
            var AuthLoginResponse = await AuthLoginRepository.LoginAsync(UserLoginRequest, CancellationToken);

            var MsgResponse = new MsgResponse<AuthTokenResponseDto?>();
                MsgResponse.Type = MessageTypeConst.QUERY;

            if(AuthLoginResponse == null){
                MsgResponse.Message = MessageDescriptionConst.INVALID_CREDENTIAL_DESCRIPTION;                
            }
            else{
                MsgResponse.Message = MessageDescriptionConst.VALID_CREDENTIAL_DESCRIPTION;
                AppUserDto AppUser = AuthMapper.AuthLoginResponseToAppUser(AuthLoginResponse.Value); 
                var AuthTokenResponse = new AuthTokenResponseDto()
                {
                    AccessToken = await GenerateTokenService.GenerateJWTToken(AppUser),
                    RefreshToken = await GenerateTokenService.GenerateRandomToken()
                };
                var Model = new Token()
                {
                    UserID = AppUser.UserID,
                    CompanyID = AppUser.CompanyID,
                    TokenRefreshRandom = AuthTokenResponse.RefreshToken,
                    TokenCreateDateTime = AppUser.CurrentDateTime,
                    TokenExpirationRandomDateTime = AppUser.ExpirationRandomDateTime,
                    TokenExpirationJWTDateTime = AppUser.ExpirationJWTDateTime
                };

               await TokenCreateRepository.CreateAsync(Model, CancellationToken);
                
               MsgResponse.Data = AuthTokenResponse;
            }
           return MsgResponse;
        } 
    }
}