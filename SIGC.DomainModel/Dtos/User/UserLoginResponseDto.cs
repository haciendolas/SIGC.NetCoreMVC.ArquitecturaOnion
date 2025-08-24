namespace SIGC.DomainModel.Dtos.User
{
    public record struct UserLoginResponseDto(
        int UserID,
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