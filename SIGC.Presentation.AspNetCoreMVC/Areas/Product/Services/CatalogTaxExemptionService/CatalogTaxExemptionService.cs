using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTaxExemption;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTaxExemptionService
{
    public class CatalogTaxExemptionService : ICatalogTaxExemptionService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "CatalogTaxExemption";

        public CatalogTaxExemptionService(IApiServiceFactory ApiServiceFactory)
        {
            ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }
       
        public async Task<ApiResponse<object?>> CatalogTaxExemptionCreate(CatalogTaxExemptionCreateUpdateRequestModel Request)
        {
            return await ApiService.PostAsync<CatalogTaxExemptionCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogTaxExemptionCreate", Request);
        }
        /*
         public async Task<ApiResponse<object?>> CatalogPriceDelete(int CatalogPriceID)
         {
             return await ApiService.DeleteAsync<ApiResponse<object?>>($"{Controller}/CatalogPriceDelete/{CatalogPriceID}");
         }

         public async Task<ApiResponse<List<CatalogTaxListResponseModel>>> CatalogTaxList(int CatalogID)
         {
             return await ApiService.GetAsync<ApiResponse<List<CatalogTaxListResponseModel>>>($"{Controller}/CatalogTaxList/{CatalogID}");
         }
         */
    }
}