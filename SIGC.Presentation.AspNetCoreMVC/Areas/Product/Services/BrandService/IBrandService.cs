using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Brand;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.BrandService
{
    public interface IBrandService
    {
        Task<ApiResponse<List<BrandListResponseModel>>> BrandList();
    }
}