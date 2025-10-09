using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Company;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
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
        public async Task<ApiResponse<List<CompanyListResponseModel>>> CompanyList(int CompanyIDRegister)
        {
            return await ApiService.GetAsync<ApiResponse<List<CompanyListResponseModel>>>($"{Controller}/CompanyList/{CompanyIDRegister}");
        }
    }
}