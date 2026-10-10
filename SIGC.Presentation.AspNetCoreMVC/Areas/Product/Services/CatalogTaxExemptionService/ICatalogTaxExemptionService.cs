using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTaxExemption;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTaxExemptionService
{
    public interface ICatalogTaxExemptionService
    {      
        Task<ApiResponse<object?>> CatalogTaxExemptionCreate(CatalogTaxExemptionCreateUpdateRequestModel Request);    
        //Task<ApiResponse<object?>> CatalogPriceDelete(int CatalogPriceID);       
        //Task<ApiResponse<List<CatalogTaxListResponseModel>>> CatalogTaxList(int CatalogID);
    }
}