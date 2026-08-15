using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.PharmaceuticalForm;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PharmaceuticalFormService
{
    public class PharmaceuticalFormService : IPharmaceuticalFormService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "PharmaceuticalForm";

        public PharmaceuticalFormService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<PharmaceuticalFormListResponseModel>>> PharmaceuticalFormList()
        {
            return await ApiService.GetAsync<ApiResponse<List<PharmaceuticalFormListResponseModel>>>($"{Controller}/PharmaceuticalFormList");
        }
    }
}