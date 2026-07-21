using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Catalog;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogService
{
    public class CatalogService : ICatalogService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Catalog";

        public CatalogService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<PaginationResultModel<CatalogPaginationResponseModel>>> CatalogPagination(CatalogPaginationRequestModel Request)
        {
            return await ApiService.PostAsync<string, ApiResponse<PaginationResultModel<CatalogPaginationResponseModel>>>($"{Controller}/CatalogPagination", null, Request);
        }
    }
}