using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.PrescriptionType;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PrescriptionTypeService
{
    public class PrescriptionTypeService : IPrescriptionTypeService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "PrescriptionType";

        public PrescriptionTypeService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<PrescriptionTypeListResponseModel>>> PrescriptionTypeList()
        {
            return await ApiService.GetAsync<ApiResponse<List<PrescriptionTypeListResponseModel>>>($"{Controller}/PrescriptionTypeList");
        }
    }
}