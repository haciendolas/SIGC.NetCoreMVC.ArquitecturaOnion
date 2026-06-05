using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IApiService ApiService;
        private readonly string Controller = "Category";

        public CategoryService(IApiServiceFactory ApiServiceFactory)
        {
            this.ApiService = ApiServiceFactory.Create(ConstantsHelper.HttpClientNames.ApiCommerce360);
        }

        public async Task<ApiResponse<object?>> CategoryCreate(CategoryCreateUpdateRequestModel Request)
        {
            return await ApiService.PostFormDataAsync<CategoryCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CategoryCreate", Request);
        }
        public async Task<ApiResponse<object?>> CategoryUpdate(CategoryCreateUpdateRequestModel Request)
        {
            return await ApiService.PutFormDataAsync<CategoryCreateUpdateRequestModel, ApiResponse<object?>>($"{Controller}/CategoryUpdate", Request);
        }
        public async Task<ApiResponse<object?>> CategoryChangeState(CategoryChangeStateRequestModel Request)
        {
            return await ApiService.PutAsync<CategoryChangeStateRequestModel, ApiResponse<object?>>($"{Controller}/CategoryChangeState", Request);
        }
        public async Task<ApiResponse<CategoryGetResponseModel?>> CategoryGet(int CategoryID)
        {
            return await ApiService.GetAsync<ApiResponse<CategoryGetResponseModel?>>($"{Controller}/CategoryGet/{CategoryID}");
        }
        public async Task<ApiResponse<PaginationResultModel<CategoryPaginationResponseModel>>> CategoryPagination(CategoryPaginationRequestModel Request)
        {
            return await ApiService.PostAsync<string, ApiResponse<PaginationResultModel<CategoryPaginationResponseModel>>>($"{Controller}/CategoryPagination", null, Request);
        }
    }
}