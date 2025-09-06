using Microsoft.AspNetCore.Authentication;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models.Auth;
using SIGC.Presentation.AspNetCoreMVC.Services.AuthService;
using System.Net;
using System.Net.Http.Headers;

namespace SIGC.Presentation.AspNetCoreMVC.Services
{
    public class AccessTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService AuthService;
        public AccessTokenHandler(IHttpContextAccessor httpContextAccessor, IAuthService AuthService)
        {
            _httpContextAccessor = httpContextAccessor;
            this.AuthService = AuthService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = _httpContextAccessor?.HttpContext?.Session.GetString(ConstantsHelper.SessionKeys.AccessToken);
            if (!string.IsNullOrEmpty(accessToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await base.SendAsync(request, cancellationToken);

            // Si recibimos 401, intentamos refrescar token y reintentar
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var RefreshToken = _httpContextAccessor?.HttpContext?.Session.GetString(ConstantsHelper.SessionKeys.RefreshToken);
                var AccessToken = _httpContextAccessor?.HttpContext?.Session.GetString(ConstantsHelper.SessionKeys.AccessToken);
                if (string.IsNullOrEmpty(RefreshToken) || string.IsNullOrEmpty(AccessToken))
                    //throw new SessionExpiredException("El refresh token no está disponible. La sesión ha expirado.");
                    return response; // El refresh token no está disponible. La sesión ha expirado

                var ApiResponse = await AuthService.Refresh(new AuthRefreshTokenRequestModel
                {
                    RefreshToken = RefreshToken,
                    AccessToken = AccessToken
                });

                if (ApiResponse.Data is not null)
                {
                    // Actualizar tokens en sesión
                    _httpContextAccessor?.HttpContext?.Session.SetString(ConstantsHelper.SessionKeys.AccessToken, ApiResponse.Data.Value.AccessToken);
                    _httpContextAccessor?.HttpContext?.Session.SetString(ConstantsHelper.SessionKeys.RefreshToken, ApiResponse.Data.Value.RefreshToken);

                    // Actualizar header con nuevo access token
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ApiResponse.Data.Value.AccessToken);

                    // Reintentar la petición original con el nuevo token
                    response.Dispose(); // Liberar respuesta anterior
                    response = await base.SendAsync(request, cancellationToken);
                }
            }
          
            return response;
        }
    }
}
