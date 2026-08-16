using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Presentation;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PresentationService
{
    public interface IPresentationService
    {
        Task<ApiResponse<List<PresentationListResponseModel>>> PresentationList(int UnitMeasureID);
    }
}