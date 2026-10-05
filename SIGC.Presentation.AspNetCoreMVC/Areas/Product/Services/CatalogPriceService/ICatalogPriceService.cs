using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPrice;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogPriceService
{
    public interface ICatalogPriceService
    {
        Task<ApiResponse<object?>> CatalogPriceCreate(CatalogPriceCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CatalogPriceChangeState(CatalogPriceChangeStateRequestModel Request);
        Task<ApiResponse<List<CatalogPriceListResponseModel>>> CatalogPriceList(int CatalogID);
    }
}