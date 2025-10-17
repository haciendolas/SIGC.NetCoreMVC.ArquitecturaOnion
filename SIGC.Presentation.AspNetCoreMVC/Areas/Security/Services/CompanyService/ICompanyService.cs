using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Company;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<ApiResponse<object?>> CompanyCreate(CompanyCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CompanyUpdate(CompanyCreateUpdateRequestModel Request);
        Task<ApiResponse<object?>> CompanyChangeState(CompanyChangeStateRequestModel Request);
        Task<ApiResponse<CompanyGetResponseModel?>> CompanyGet(int CompanyID);
        Task<ApiResponse<PaginationResultModel<CompanyPaginationResponseModel>>> CompanyPagination(CompanyPaginationRequestModel Request);
        Task<ApiResponse<List<CompanyListResponseModel>>> CompanyList(int CompanyIDRegister);
    }
}
