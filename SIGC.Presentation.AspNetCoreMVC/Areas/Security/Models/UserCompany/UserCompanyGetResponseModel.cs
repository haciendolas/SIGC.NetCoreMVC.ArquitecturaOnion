namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.UserCompany
{
    public record struct UserCompanyGetResponseModel
    (
       int UserID,
       string UserFirstName,
       string UserLastName,
       string UserName,
       string UserPassword,
       string UserMail,
       string UserPhoto,
       string UserUrl,
       short StateID,
       List<int> RoleIDs
    );
}