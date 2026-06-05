using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.User;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UserService
{
    public interface IUserService
    {
        Task<ApiResponse<object?>> UserCreate(UserCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> UserUpdate(UserCreateUpdateRequestModel Request);
        Task<ApiResponse<PaginationResultModel<UserPaginationResponseModel>>> UserPagination(UserPaginationRequestModel Request);
    }
}