namespace SIGC.DomainModel.Dtos
{
    public record struct AppUserDto(
       int UserID,
       string UserName,
       string UserFirstName,
       string UserLastName,
       string UserMail,
       int CompanyID,
       short IdiomID,
       string CompanyDocumentNumber,
       string CompanyTradeName,
       string CompanySocialReason,
       string RoleCodes
    );
}