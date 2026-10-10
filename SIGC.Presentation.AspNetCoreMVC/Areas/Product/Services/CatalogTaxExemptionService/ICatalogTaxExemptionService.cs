using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTaxExemption;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTaxExemptionService
{
    public interface ICatalogTaxExemptionService
    {      
        Task<ApiResponse<object?>> CatalogTaxExemptionCreate(CatalogTaxExemptionCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CatalogTaxExemptionChangeState(CatalogTaxExemptionChangeStateRequestModel Request);
        Task<ApiResponse<object?>> CatalogTaxExemptionDelete(int CatalogTaxExemptionID);       
        Task<ApiResponse<List<CatalogTaxExemptionListResponseModel>>> CatalogTaxExemptionList(int CatalogID);
    }
}