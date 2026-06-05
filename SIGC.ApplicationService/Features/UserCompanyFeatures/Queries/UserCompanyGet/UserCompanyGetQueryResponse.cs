namespace SIGC.ApplicationService.Features.UserCompanyFeatures.Queries.UserCompanyGet
{
    public record struct UserCompanyGetQueryResponse
   (   int UserID,
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