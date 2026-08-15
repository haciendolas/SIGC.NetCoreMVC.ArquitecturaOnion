using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.PriceType;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PriceTypeService
{
    public class PriceTypeService : IPriceTypeService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "PriceType";

        public PriceTypeService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<PriceTypeListResponseModel>>> PriceTypeList()
        {
            return await ApiService.GetAsync<ApiResponse<List<PriceTypeListResponseModel>>>($"{Controller}/PriceTypeList");
        }
    }
}