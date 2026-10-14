using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogStock;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogStockService
{
    public class CatalogStockService : ICatalogStockService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "CatalogStock";

        public CatalogStockService(IApiServiceFactory ApiServiceFactory)
        {
            ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }
       
        public async Task<ApiResponse<object?>> CatalogStockCreate(CatalogStockCreateUpdateRequestModel Request)
        {
            return await ApiService.PostAsync<CatalogStockCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogStockCreate", Request);
        } 
        /*
        public async Task<ApiResponse<object?>> CatalogTaxUpdate(CatalogTaxCreateUpdateRequestModel Request)
        {
            return await ApiService.PutAsync<CatalogTaxCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogTaxUpdate", Request);
        }
        public async Task<ApiResponse<object?>> CatalogTaxChangeState(CatalogTaxChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<CatalogTaxChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogTaxChangeState", Request);
        }
        public async Task<ApiResponse<List<CatalogTaxListResponseModel>>> CatalogTaxList(int CatalogID)
        {
            return await ApiService.GetAsync<ApiResponse<List<CatalogTaxListResponseModel>>>($"{Controller}/CatalogTaxList/{CatalogID}");
        }
        */
    }
}