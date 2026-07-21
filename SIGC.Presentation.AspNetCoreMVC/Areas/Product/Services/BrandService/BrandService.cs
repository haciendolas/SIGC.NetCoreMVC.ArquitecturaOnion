using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Brand;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Brand";

        public BrandService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<BrandListResponseModel>>> BrandList()
        {
            return await ApiService.GetAsync<ApiResponse<List<BrandListResponseModel>>>($"{Controller}/BrandList");
        }
    }
}