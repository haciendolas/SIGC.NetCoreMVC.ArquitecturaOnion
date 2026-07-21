using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogType;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTypeService
{
    public class CatalogTypeService : ICatalogTypeService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "CatalogType";

        public CatalogTypeService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<CatalogTypeListResponseModel>>> CatalogTypeList()
        {
            return await ApiService.GetAsync<ApiResponse<List<CatalogTypeListResponseModel>>>($"{Controller}/CatalogTypeList");
        }
    }
}