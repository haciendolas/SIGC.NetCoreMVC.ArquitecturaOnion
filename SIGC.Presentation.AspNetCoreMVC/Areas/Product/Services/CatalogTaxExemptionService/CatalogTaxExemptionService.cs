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
        public async Task<ApiResponse<object?>> CatalogTaxExemptionChangeState(CatalogTaxExemptionChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<CatalogTaxExemptionChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogTaxExemptionChangeState", Request);
        }        
        public async Task<ApiResponse<object?>> CatalogTaxExemptionDelete(int CatalogTaxExemptionID)
        {
           return await ApiService.DeleteAsync<ApiResponse<object?>>($"{Controller}/CatalogTaxExemptionDelete/{CatalogTaxExemptionID}");
        }       
        public async Task<ApiResponse<List<CatalogTaxExemptionListResponseModel>>> CatalogTaxExemptionList(int CatalogID)
         {
             return await ApiService.GetAsync<ApiResponse<List<CatalogTaxExemptionListResponseModel>>>($"{Controller}/CatalogTaxExemptionList/{CatalogID}");
         }
     
    }
}