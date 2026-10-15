using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogStock;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogStockService
{
    public interface ICatalogStockService
    {      
        Task<ApiResponse<object?>> CatalogStockCreate(CatalogStockCreateUpdateRequestModel Request); 
        Task<ApiResponse<object?>> CatalogStockUpdate(CatalogStockCreateUpdateRequestModel Request);
        Task<ApiResponse<List<CatalogStockListResponseModel>>> CatalogStockList(int CatalogID,int? EstablishmentID);        
    }
}