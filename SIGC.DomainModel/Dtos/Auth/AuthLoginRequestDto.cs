namespace SIGC.DomainModel.Dtos.Auth
{
    public record struct AuthLoginRequestDto(
        string CompanyDocumentNumber,
        string UserName,
        string UserPassword
    );
}