using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Page;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageService
{
    public class PageService : IPageService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Page";

        public PageService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<PageListResponseModel>>> PageList()
        {
           return await ApiService.GetAsync<ApiResponse<List<PageListResponseModel>>>($"{Controller}/PageList");
        }
    }
}
