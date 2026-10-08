using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTax;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTaxService
{
    public interface ICatalogTaxService
    {
        /*
        Task<ApiResponse<object?>> CatalogPriceCreate(CatalogPriceCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CatalogPriceUpdate(CatalogPriceCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CatalogPriceChangeState(CatalogPriceChangeStateRequestModel Request);
        Task<ApiResponse<object?>> CatalogPriceDelete(int CatalogPriceID);
        */
        Task<ApiResponse<List<CatalogTaxListResponseModel>>> CatalogTaxList(int CatalogID);
    }
}