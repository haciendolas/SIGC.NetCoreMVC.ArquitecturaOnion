namespace SIGC.DomainModel.Dtos.Token
{
    public record struct TokenGetExpirationResquestDto
    (
        int UserID,
        string TokenRefreshRandom,
        DateTime TokenExpirationDateTime
    );
}