using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.ActiveIngredient;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.ActiveIngredientService
{
    public class ActiveIngredientService : IActiveIngredientService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "ActiveIngredient";

        public ActiveIngredientService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<ActiveIngredientListResponseModel>>> ActiveIngredientList()
        {
            return await ApiService.GetAsync<ApiResponse<List<ActiveIngredientListResponseModel>>>($"{Controller}/ActiveIngredientList");
        }
    }
}