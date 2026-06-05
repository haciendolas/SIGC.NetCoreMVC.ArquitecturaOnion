using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.RoleService
{
    public interface IRoleService
    {
        Task<ApiResponse<object?>> RoleCreate(RoleCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> RoleUpdate(RoleCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> RoleChangeState(RoleChangeStateRequestModel Request);
        Task<ApiResponse<RoleGetResponseModel?>> RoleGet(int RoleID);
        Task<ApiResponse<List<RoleListResponseModel>>> RoleList(int CompanyID);
        Task<ApiResponse<PaginationResultModel<RolePaginationResponseModel>>> RolePagination(RolePaginationRequestModel Request);   
    }
}