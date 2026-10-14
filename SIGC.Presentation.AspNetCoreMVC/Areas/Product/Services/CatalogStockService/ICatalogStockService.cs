using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogStock;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogStockService
{
    public interface ICatalogStockService
    {      
        Task<ApiResponse<object?>> CatalogStockCreate(CatalogStockCreateUpdateRequestModel Request);    
        /*
        Task<ApiResponse<object?>> CatalogTaxUpdate(CatalogTaxCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CatalogTaxChangeState(CatalogTaxChangeStateRequestModel Request);          
        Task<ApiResponse<List<CatalogTaxListResponseModel>>> CatalogTaxList(int CatalogID);
        */
    }
}