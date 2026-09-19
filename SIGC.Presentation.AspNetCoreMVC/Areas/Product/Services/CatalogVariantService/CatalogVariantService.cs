using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogVariant;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogVariantService
{
    public class CatalogVariantService : ICatalogVariantService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "CatalogVariant";

        public CatalogVariantService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }
        public async Task<ApiResponse<object?>> CatalogVariantCreate(CatalogVariantCreateUpdateRequestModel Request)
        {
            return await ApiService.PostAsync<CatalogVariantCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogVariantCreate", Request);
        }       
        public async Task<ApiResponse<object?>> CatalogVariantUpdate(CatalogVariantCreateUpdateRequestModel Request)
        {
            return await ApiService.PutAsync<CatalogVariantCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogVariantUpdate", Request);
        }
        public async Task<ApiResponse<List<CatalogVariantListResponseModel>>> CatalogVariantList(int CatalogID)
        {
            return await ApiService.GetAsync<ApiResponse<List<CatalogVariantListResponseModel>>>($"{Controller}/CatalogVariantList/{CatalogID}");
        }
    }
}