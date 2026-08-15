using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.PrescriptionType;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PrescriptionTypeService
{
    public interface IPrescriptionTypeService
    {
        Task<ApiResponse<List<PrescriptionTypeListResponseModel>>> PrescriptionTypeList();
    }
}