using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.User;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "User";

        public UserService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<object?>> UserCreate(UserCreateUpdateRequestModel Request)
        {
            return await ApiService.PostFormDataAsync<UserCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/UserCreate", Request);
        }

        public async Task<ApiResponse<object?>> UserUpdate(UserCreateUpdateRequestModel Request)
        {
            return await ApiService.PutFormDataAsync<UserCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/UserUpdate", Request);
        }

        public async Task<ApiResponse<PaginationResultModel<UserPaginationResponseModel>>> UserPagination(UserPaginationRequestModel Request)
        {
            return await ApiService.PostAsync<string, ApiResponse<PaginationResultModel<UserPaginationResponseModel>>>($"{Controller}/UserPagination", null, Request);
        }
    }
}