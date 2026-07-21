using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Manufacturer;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.ManufacturerService
{
    public class ManufacturerService : IManufacturerService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Manufacturer";

        public ManufacturerService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<ManufacturerListResponseModel>>> ManufacturerList()
        {
            return await ApiService.GetAsync<ApiResponse<List<ManufacturerListResponseModel>>>($"{Controller}/ManufacturerList");
        }
    }
}