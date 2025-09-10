using SIGC.Presentation.AspNetCoreMVC.Models.RolePermission;

namespace SIGC.Presentation.AspNetCoreMVC.Services.RolePermissionService
{
    public interface IRolePermissionService
    {
        Task<ApiResponse<List<RolePermissionListResponseModel>>> RolePermissionList(RolePermissionListRequestModel Request);
    }
}