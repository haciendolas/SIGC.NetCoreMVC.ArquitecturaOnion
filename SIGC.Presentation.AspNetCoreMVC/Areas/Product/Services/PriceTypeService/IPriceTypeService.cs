using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.PriceType;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PriceTypeService
{
    public interface IPriceTypeService
    {
        Task<ApiResponse<List<PriceTypeListResponseModel>>> PriceTypeList();
    }
}