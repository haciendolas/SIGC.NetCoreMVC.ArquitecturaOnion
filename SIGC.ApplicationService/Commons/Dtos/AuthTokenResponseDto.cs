namespace SIGC.ApplicationService.Commons.Dtos
{
   public record struct AuthTokenResponseDto(
      string AccessToken,
      string RefreshToken,
      AccountInfo AccountInfo
   );

    public record struct AccountInfo(
       string UserPhotoUrl,
       string UserFirstName,
       string UserLastName
    );
}