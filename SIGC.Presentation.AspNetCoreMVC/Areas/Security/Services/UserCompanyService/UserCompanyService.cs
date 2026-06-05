using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.UserCompany;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UserCompanyService
{
    public class UserCompanyService : IUserCompanyService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "UserCompany";

        public UserCompanyService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<object?>> UserCompanyChangeState(UserCompanyChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<UserCompanyChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/UserCompanyChangeState", Request);
        }

        public async Task<ApiResponse<UserCompanyGetResponseModel?>> UserCompanyGet(int UserID, int CompanyID)
        {
            return await ApiService.GetAsync<ApiResponse<UserCompanyGetResponseModel?>>($"{Controller}/UserCompanyGet/{UserID}/{CompanyID}");
        }
    }
}