using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPresentation;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogPresentationService
{
    public interface ICatalogPresentationService
    {
        Task<ApiResponse<object?>> CatalogPresentationCreate(CatalogPresentationCreateRequestModel Request);
        Task<ApiResponse<List<CatalogVariantListResponseModel>>> CatalogPresentationList(int CatalogID);
    }
}