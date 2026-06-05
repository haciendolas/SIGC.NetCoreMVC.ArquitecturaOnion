namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role
{
    public record struct RoleListResponseModel
    (
        int RoleID,
        string RoleCode,
        string RoleName
    );
}