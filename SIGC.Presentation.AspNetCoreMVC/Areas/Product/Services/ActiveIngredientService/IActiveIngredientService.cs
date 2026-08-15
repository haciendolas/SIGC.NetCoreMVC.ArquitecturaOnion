using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.ActiveIngredient;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.ActiveIngredientService
{
    public interface IActiveIngredientService
    {
        Task<ApiResponse<List<ActiveIngredientListResponseModel>>> ActiveIngredientList();
    }
}