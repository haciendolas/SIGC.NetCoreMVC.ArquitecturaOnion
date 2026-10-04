using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPrice;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogPriceService
{
    public class CatalogPriceService : ICatalogPriceService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "CatalogPrice";

        public CatalogPriceService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<object?>> CatalogPriceCreate(CatalogPriceCreateUpdateRequestModel Request)
        {
            return await ApiService.PostAsync<CatalogPriceCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogPriceCreate", Request);
        }
        /*
        public async Task<ApiResponse<object?>> CatalogPresentationChangeState(CatalogPresentationChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<CatalogPresentationChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogPresentationChangeState", Request);
        }
        */
        public async Task<ApiResponse<List<CatalogPriceListResponseModel>>> CatalogPriceList(int CatalogID)
        {
            return await ApiService.GetAsync<ApiResponse<List<CatalogPriceListResponseModel>>>($"{Controller}/CatalogPriceList/{CatalogID}");
        }
     
    }
}