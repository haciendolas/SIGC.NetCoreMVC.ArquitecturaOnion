using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Presentation;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PresentationService
{
    public class PresentationService : IPresentationService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Presentation";

        public PresentationService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<PresentationListResponseModel>>> PresentationList(int UnitMeasureID)
        {
            return await ApiService.GetAsync<ApiResponse<List<PresentationListResponseModel>>>($"{Controller}/PresentationList/{UnitMeasureID}");
        }
    }
}