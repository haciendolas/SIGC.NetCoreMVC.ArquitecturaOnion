using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Manufacturer;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.ManufacturerService
{
    public interface IManufacturerService
    {
        Task<ApiResponse<List<ManufacturerListResponseModel>>> ManufacturerList();
    }
}