namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role
{
    public record struct RoleGetResponseModel
    (
        int RoleID,
        string RoleCode,
        string RoleName,
        string RoleDescription,
        short StateID,
        List<RolePageGetResponseModel> Pages
    );


    public record struct RolePageGetResponseModel(
        int PageID,
        List<RoleActionGetResponseModel> Actions
    );

    public record struct RoleActionGetResponseModel(
        int PageActionID
    );
}