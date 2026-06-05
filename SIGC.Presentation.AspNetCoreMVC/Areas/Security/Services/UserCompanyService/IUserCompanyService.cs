using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.UserCompany;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UserCompanyService
{
    public interface IUserCompanyService
    {
        Task<ApiResponse<object?>> UserCompanyChangeState(UserCompanyChangeStateRequestModel Request);
        Task<ApiResponse<UserCompanyGetResponseModel?>> UserCompanyGet(int UserID,int CompanyID);
    }
}