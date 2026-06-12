using SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Warehouse;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Services.WarehouseService
{
    public interface IWarehouseService
    {
        Task<ApiResponse<object?>> WarehouseCreate(WarehouseCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> WarehouseUpdate(WarehouseCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> WarehouseChangeState(WarehouseChangeStateRequestModel Request);
        Task<ApiResponse<WarehouseGetResponseModel?>> WarehouseGet(int WarehouseID);
        Task<ApiResponse<PaginationResultModel<WarehousePaginationResponseModel>>> WarehousePagination(WarehousePaginationRequestModel Request);
    }
}