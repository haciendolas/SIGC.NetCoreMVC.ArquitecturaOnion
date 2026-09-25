using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Catalog;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogService
{
    public class CatalogService : ICatalogService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Catalog";

        public CatalogService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<object?>> CatalogCreate(CatalogCreateUpdateRequestModel Request)
        {
            return await ApiService.PostFormDataAsync<CatalogCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogCreate", Request);
        }
        public async Task<ApiResponse<object?>> CatalogUpdate(CatalogCreateUpdateRequestModel Request)
        {
            return await ApiService.PutFormDataAsync<CatalogCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogUpdate", Request);
        }
        public async Task<ApiResponse<object?>> CatalogChangeState(CatalogChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<CatalogChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/CatalogChangeState", Request);
        }
        public async Task<ApiResponse<CatalogGetResponseModel?>> CatalogGet(int CatalogID)
        {
            return await ApiService.GetAsync<ApiResponse<CatalogGetResponseModel?>>($"{Controller}/CatalogGet/{CatalogID}");
        }
        public async Task<ApiResponse<PaginationResultModel<CatalogPaginationResponseModel>>> CatalogPagination(CatalogPaginationRequestModel Request)
        {
            return await ApiService.PostAsync<string, ApiResponse<PaginationResultModel<CatalogPaginationResponseModel>>>($"{Controller}/CatalogPagination", null, Request);
        }
    }
}