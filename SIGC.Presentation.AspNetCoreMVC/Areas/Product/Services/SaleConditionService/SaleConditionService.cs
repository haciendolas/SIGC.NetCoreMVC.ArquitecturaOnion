using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.SaleCondition;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.SaleConditionService
{
    public class SaleConditionService : ISaleConditionService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "SaleCondition";

        public SaleConditionService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<SaleConditionListResponseModel>>> SaleConditionList()
        {
            return await ApiService.GetAsync<ApiResponse<List<SaleConditionListResponseModel>>>($"{Controller}/SaleConditionList");
        }
    }
}