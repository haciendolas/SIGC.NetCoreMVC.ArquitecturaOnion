using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models.Auth;

namespace SIGC.Presentation.AspNetCoreMVC.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IApiService ApiService;

        public AuthService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiAuth360);
        } 

        public async Task<ApiResponse<AuthTokenResponseModel?>> SignIn(AuthLoginTokenRequestModel Request)
        {
            return await ApiService.PostAsync<AuthLoginTokenRequestModel, ApiResponse<AuthTokenResponseModel?>>("Auth/SignIn", Request);
        }

        public async Task<ApiResponse<AuthTokenResponseModel?>> Refresh(AuthRefreshTokenRequestModel Request)
        {
            return await ApiService.PostAsync<AuthRefreshTokenRequestModel, ApiResponse<AuthTokenResponseModel?>>("Auth/Refresh", Request);          
        }
    }
}