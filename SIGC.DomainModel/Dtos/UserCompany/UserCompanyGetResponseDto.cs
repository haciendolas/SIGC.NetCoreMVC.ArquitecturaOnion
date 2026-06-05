namespace SIGC.DomainModel.Dtos.UserCompany
{
    public record struct UserCompanyGetResponseDto
    (
       int UserID,
       string UserFirstName,
       string UserLastName,
       string UserName,
       string UserPassword,
       string UserMail,
       string UserPhoto,
       short StateID,
       List<int> RoleIDs
    );    
}