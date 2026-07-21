using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ApiResponse<object?>> CategoryCreate(CategoryCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CategoryUpdate(CategoryCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CategoryChangeState(CategoryChangeStateRequestModel Request);
        Task<ApiResponse<CategoryGetResponseModel?>> CategoryGet(int CategoryID);
        Task<ApiResponse<List<CategoryListResponseModel>>> CategoryList();
        Task<ApiResponse<PaginationResultModel<CategoryPaginationResponseModel>>> CategoryPagination(CategoryPaginationRequestModel Request);
    }
}