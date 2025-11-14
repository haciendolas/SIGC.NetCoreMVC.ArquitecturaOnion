using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Company;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.CompanyService
{
    public class CompanyService : ICompanyService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Company";

        public CompanyService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<object?>> CompanyCreate(CompanyCreateUpdateRequestModel Request)
        {
            /*
            var Files = new Dictionary<string, (Stream Stream, string FileName, string ContentType)>();
            if (Request.FormFile != null)
            {
                Files.Add("FormFile", (Request.FormFile.OpenReadStream(), Request.FormFile.FileName, Request.FormFile.ContentType));
            }
            */
           // return await ApiService.PostFormDataAsync<CompanyCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CompanyCreate", Request, Files);
            return await ApiService.PostFormDataAsync<CompanyCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CompanyCreate", Request);
        }
        public async Task<ApiResponse<object?>> CompanyUpdate(CompanyCreateUpdateRequestModel Request)
        {
            return await ApiService.PutAsync<CompanyCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CompanyUpdate", Request);
        }
        public async Task<ApiResponse<object?>> CompanyChangeState(CompanyChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<CompanyChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/CompanyChangeState", Request);
        }
        public async Task<ApiResponse<CompanyGetResponseModel?>> CompanyGet(int CompanyID)
        {
            return await ApiService.GetAsync<ApiResponse<CompanyGetResponseModel?>>($"{Controller}/CompanyGet/{CompanyID}");
        }
        public async Task<ApiResponse<PaginationResultModel<CompanyPaginationResponseModel>>> CompanyPagination(CompanyPaginationRequestModel Request)
        {
            return await ApiService.PostAsync<string, ApiResponse<PaginationResultModel<CompanyPaginationResponseModel>>>($"{Controller}/CompanyPagination", null, Request);
        }

        public async Task<ApiResponse<List<CompanyListResponseModel>>> CompanyList(int CompanyIDRegister)
        {
            return await ApiService.GetAsync<ApiResponse<List<CompanyListResponseModel>>>($"{Controller}/CompanyList/{CompanyIDRegister}");
        }
    }
}