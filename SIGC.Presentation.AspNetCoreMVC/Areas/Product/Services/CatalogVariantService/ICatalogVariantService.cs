using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogVariant;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogVariantService
{
    public interface ICatalogVariantService
    {
        Task<ApiResponse<object?>> CatalogVariantCreate(CatalogVariantCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CatalogVariantUpdate(CatalogVariantCreateUpdateRequestModel Request);
        Task<ApiResponse<List<CatalogVariantListResponseModel>>> CatalogVariantList(int CatalogID);
    }
}