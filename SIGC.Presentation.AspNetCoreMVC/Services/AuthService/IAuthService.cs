using SIGC.Presentation.AspNetCoreMVC.Models.Auth;

namespace SIGC.Presentation.AspNetCoreMVC.Services.AuthService
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthTokenResponseModel?>> SignIn(AuthLoginTokenRequestModel Request);
        Task<ApiResponse<AuthTokenResponseModel?>> Refresh(AuthRefreshTokenRequestModel Request);
    }
}