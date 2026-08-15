using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.TherapeuticAction;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.TherapeuticActionService
{
    public class TherapeuticActionService : ITherapeuticActionService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "TherapeuticAction";

        public TherapeuticActionService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<TherapeuticActionListResponseModel>>> TherapeuticActionList()
        {
            return await ApiService.GetAsync<ApiResponse<List<TherapeuticActionListResponseModel>>>($"{Controller}/TherapeuticActionList");
        }
    }
}