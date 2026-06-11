using SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Warehouse;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Services.WarehouseService
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Warehouse";

        public WarehouseService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }
        public async Task<ApiResponse<object?>> WarehouseCreate(WarehouseCreateUpdateRequestModel Request)
        {
            return await ApiService.PostAsync<WarehouseCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/WarehouseCreate", Request);
        }
        public async Task<ApiResponse<WarehouseGetResponseModel?>> WarehouseGet(int WarehouseID)
        {
            return await ApiService.GetAsync<ApiResponse<WarehouseGetResponseModel?>>($"{Controller}/WarehouseGet/{WarehouseID}");
        }
        public async Task<ApiResponse<PaginationResultModel<WarehousePaginationResponseModel>>> WarehousePagination(WarehousePaginationRequestModel Request)
        {
            return await ApiService.PostAsync<string, ApiResponse<PaginationResultModel<WarehousePaginationResponseModel>>>($"{Controller}/WarehousePagination", null, Request);
        }
    }
}