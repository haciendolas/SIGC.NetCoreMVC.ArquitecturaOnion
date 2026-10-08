using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTax;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTaxService
{
    public class CatalogTaxService : ICatalogTaxService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "CatalogTax";

        public CatalogTaxService(IApiServiceFactory ApiServiceFactory)
        {
            ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }
        /*
        public async Task<ApiResponse<object?>> CatalogPriceCreate(CatalogPriceCreateUpdateRequestModel Request)
        {
            return await ApiService.PostAsync<CatalogPriceCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogPriceCreate", Request);
        }
        public async Task<ApiResponse<object?>> CatalogPriceUpdate(CatalogPriceCreateUpdateRequestModel Request)
        {
            return await ApiService.PutAsync<CatalogPriceCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogPriceUpdate", Request);
        }
        public async Task<ApiResponse<object?>> CatalogPriceChangeState(CatalogPriceChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<CatalogPriceChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogPriceChangeState", Request);
        }
        public async Task<ApiResponse<object?>> CatalogPriceDelete(int CatalogPriceID)
        {
            return await ApiService.DeleteAsync<ApiResponse<object?>>($"{Controller}/CatalogPriceDelete/{CatalogPriceID}");
        }
        */
        public async Task<ApiResponse<List<CatalogTaxListResponseModel>>> CatalogTaxList(int CatalogID)
        {
            return await ApiService.GetAsync<ApiResponse<List<CatalogTaxListResponseModel>>>($"{Controller}/CatalogTaxList/{CatalogID}");
        }
    }
}