using SIGC.Presentation.AspNetCoreMVC.Areas.Accounting.Models.Tax;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Accounting.Services.TaxService
{
    public class TaxService : ITaxService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Tax";

        public TaxService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<TaxListResponseModel>>> TaxList()
        {
            return await ApiService.GetAsync<ApiResponse<List<TaxListResponseModel>>>($"{Controller}/TaxList");
        }
    }
}