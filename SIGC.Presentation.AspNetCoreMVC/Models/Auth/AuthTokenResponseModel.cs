namespace SIGC.Presentation.AspNetCoreMVC.Models.Auth
{
   public record struct AuthTokenResponseModel(
      string AccessToken,
      string RefreshToken  
   );
}