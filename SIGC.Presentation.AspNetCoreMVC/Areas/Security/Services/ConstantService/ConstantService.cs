using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Constant;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.ConstantService
{
    public class ConstantService : IConstantService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = " Constant";

        public ConstantService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<ConstantListResponseModel>>> ConstantList(string ConstantClassConcat)
        {
            return await ApiService.GetAsync<ApiResponse<List<ConstantListResponseModel>>>($"{Controller}/ConstantList?ConstantClassConcat={ConstantClassConcat}");
        }
    }
}