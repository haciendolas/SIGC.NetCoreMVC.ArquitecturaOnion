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
       
        public async Task<ApiResponse<object?>> CatalogTaxCreate(CatalogTaxCreateUpdateRequestModel Request)
        {
            return await ApiService.PostAsync<CatalogTaxCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogTaxCreate", Request);
        } 
        public async Task<ApiResponse<object?>> CatalogTaxUpdate(CatalogTaxCreateUpdateRequestModel Request)
        {
            return await ApiService.PutAsync<CatalogTaxCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogTaxUpdate", Request);
        }
        public async Task<ApiResponse<object?>> CatalogTaxChangeState(CatalogTaxChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<CatalogTaxChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogTaxChangeState", Request);
        }/*
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