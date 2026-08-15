using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.UnitMeasure;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.UnitMeasureService
{
    public class UnitMeasureService : IUnitMeasureService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "UnitMeasure";

        public UnitMeasureService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<UnitMeasureListResponseModel>>> UnitMeasureList()
        {
            return await ApiService.GetAsync<ApiResponse<List<UnitMeasureListResponseModel>>>($"{Controller}/UnitMeasureList");
        }
    }
}