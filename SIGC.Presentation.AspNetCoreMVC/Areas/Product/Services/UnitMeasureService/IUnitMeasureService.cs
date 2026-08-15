using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.UnitMeasure;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.UnitMeasureService
{
    public interface IUnitMeasureService
    {
        Task<ApiResponse<List<UnitMeasureListResponseModel>>> UnitMeasureList();
    }
}