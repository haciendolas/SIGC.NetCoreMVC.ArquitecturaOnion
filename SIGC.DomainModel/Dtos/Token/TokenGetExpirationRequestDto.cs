namespace SIGC.DomainModel.Dtos.Token
{
    public record struct TokenGetExpirationRequestDto
    (
        int UserID,
        string TokenRefreshRandom,
        DateTime TokenExpirationDateTime
    );
}