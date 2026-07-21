using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Catalog;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogService
{
    public interface ICatalogService
    {
        Task<ApiResponse<PaginationResultModel<CatalogPaginationResponseModel>>> CatalogPagination(CatalogPaginationRequestModel Request);
    }
}