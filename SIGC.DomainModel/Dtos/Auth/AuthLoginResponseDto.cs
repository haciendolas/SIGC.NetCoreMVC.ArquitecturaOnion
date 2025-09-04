namespace SIGC.DomainModel.Dtos.Auth
{
    public record struct AuthLoginResponseDto(
        int UserID,
        string UserName,
        string UserFirstName,
        string UserLastName,
        string UserMail,
        int CompanyID,
        string CompanyDocumentNumber,
        string CompanyTradeName,
        string CompanySocialReason,
        short StateID
   );
}