using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Role";

        public RoleService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        } 

        public  async Task<ApiResponse<PaginationResultModel<RolePaginationResponseModel>>> RolePagination(RolePaginationRequestModel Request)
        {
            return await ApiService.PostAsync<string, ApiResponse<PaginationResultModel<RolePaginationResponseModel>>>($"{Controller}/RolePagination", null ,Request);
        }

        public async Task<ApiResponse<object?>> RoleChangeState(RoleChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<RoleChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/RoleChangeState", Request);
        }

        public async Task<ApiResponse<object?>> RoleCreate(RoleCreateUpdateRequestModel Request)
        {
            return await ApiService.PostAsync<RoleCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/RoleCreate", Request);
        }
    }
}