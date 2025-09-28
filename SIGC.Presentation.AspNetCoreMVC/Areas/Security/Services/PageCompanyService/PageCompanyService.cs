using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Page;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageCompanyService
{
    public class PageCompanyService : IPageCompanyService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "PageCompany";

        public PageCompanyService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }
        public async Task<ApiResponse<List<PageListResponseModel>>> PageCompanyList(int CompanyID)
        {
            return await ApiService.GetAsync<ApiResponse<List<PageListResponseModel>>>($"{Controller}/PageCompanyList/{CompanyID}");
        }
    }
}