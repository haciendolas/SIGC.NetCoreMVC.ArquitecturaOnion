namespace SIGC.Presentation.AspNetCoreMVC.Models.Auth
{
   public record struct AuthRefreshTokenRequestModel(
      string AccessToken,
      string RefreshToken  
   );
}