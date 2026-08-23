using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Attribute;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.AttributeService
{
    public class AttributeService : IAttributeService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Attribute";

        public AttributeService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<AttributeListResponseModel>>> AttributeValueList(bool? AttributeIsVariant)
        { 
            return await ApiService.GetAsync<ApiResponse<List<AttributeListResponseModel>>>($"{Controller}/AttributeValueList",new {AttributeIsVariant});
        }
    }
}