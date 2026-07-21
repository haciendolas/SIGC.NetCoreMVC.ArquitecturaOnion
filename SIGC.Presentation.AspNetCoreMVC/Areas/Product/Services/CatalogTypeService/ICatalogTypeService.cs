using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogType;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTypeService
{
    public interface ICatalogTypeService
    {
        Task<ApiResponse<List<CatalogTypeListResponseModel>>> CatalogTypeList();
    }
}