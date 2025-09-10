using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models.RolePermission;

namespace SIGC.Presentation.AspNetCoreMVC.Services.RolePermissionService
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "RolePermission";

        public RolePermissionService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<RolePermissionListResponseModel>>> RolePermissionList(RolePermissionListRequestModel Request)
        {
            return await ApiService.PostAsync<RolePermissionListRequestModel, ApiResponse<List<RolePermissionListResponseModel>>>($"{Controller}/RolePermissionList", Request);
        }
    }
}
