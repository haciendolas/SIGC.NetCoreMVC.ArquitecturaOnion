using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.TherapeuticAction;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.TherapeuticActionService
{
    public interface ITherapeuticActionService
    {
        Task<ApiResponse<List<TherapeuticActionListResponseModel>>> TherapeuticActionList();
    }
}