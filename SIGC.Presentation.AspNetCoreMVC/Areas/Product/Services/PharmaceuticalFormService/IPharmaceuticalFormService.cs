using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.PharmaceuticalForm;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PharmaceuticalFormService
{
    public interface IPharmaceuticalFormService
    {
        Task<ApiResponse<List<PharmaceuticalFormListResponseModel>>> PharmaceuticalFormList();
    }
}