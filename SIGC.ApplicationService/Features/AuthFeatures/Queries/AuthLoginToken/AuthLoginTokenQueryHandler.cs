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
         IAuthMapper AuthMapper,
         IFileStorageService FileStorageService 
    ) : IRequestHandler<AuthLoginTokenQueryRequest, MsgResponse<AuthTokenResponseDto?>>{

        private string FolderUser = "User";
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

                FileEntryDto FileEntry = new FileEntryDto(AuthLoginResponse.Value.UserPhoto, $"{FolderUser}/{AuthLoginResponse.Value.UserPhoto}");

                var AuthTokenResponse = new AuthTokenResponseDto()
                {
                    AccessToken = await GenerateTokenService.GenerateJWTToken(AppUser),
                    RefreshToken = await GenerateTokenService.GenerateRandomToken(),
                    AccountInfo = new AccountInfo
                    {
                        UserPhotoUrl = string.IsNullOrWhiteSpace(AuthLoginResponse.Value.UserPhoto) ? "" : FileStorageService.GetFileUrl(FileEntry),
                        UserFirstName = AuthLoginResponse.Value.UserFirstName,
                        UserLastName = AuthLoginResponse.Value.UserLastName,
                    }
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