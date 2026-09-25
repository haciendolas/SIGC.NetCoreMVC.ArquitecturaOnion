using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Catalog;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogService
{
    public interface ICatalogService
    {
        Task<ApiResponse<object?>> CatalogCreate(CatalogCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CatalogUpdate(CatalogCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CatalogChangeState(CatalogChangeStateRequestModel Request);
        Task<ApiResponse<CatalogGetResponseModel?>> CatalogGet(int CatalogID);
        Task<ApiResponse<PaginationResultModel<CatalogPaginationResponseModel>>> CatalogPagination(CatalogPaginationRequestModel Request);
    }
}