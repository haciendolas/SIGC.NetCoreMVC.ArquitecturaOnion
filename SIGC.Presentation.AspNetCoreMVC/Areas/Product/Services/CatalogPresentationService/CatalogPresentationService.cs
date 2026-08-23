using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPresentation;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogPresentationService
{
    public class CatalogPresentationService : ICatalogPresentationService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "CatalogPresentation";

        public CatalogPresentationService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<List<CatalogVariantListResponseModel>>> CatalogPresentationList(int CatalogID)
        {
            return await ApiService.GetAsync<ApiResponse<List<CatalogVariantListResponseModel>>>($"{Controller}/CatalogPresentationList/{CatalogID}");
        }
    }
}