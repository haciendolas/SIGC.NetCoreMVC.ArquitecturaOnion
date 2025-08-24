namespace SIGC.DomainModel.Dtos.User
{
    public record struct UserLoginRequestDto(
        string CompanyDocumentNumber,
        string UserName,
        string UserPassword
    );
}