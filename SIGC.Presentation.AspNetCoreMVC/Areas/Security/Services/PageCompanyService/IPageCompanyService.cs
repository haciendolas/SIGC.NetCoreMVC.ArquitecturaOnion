using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Page;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.PageCompany;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageCompanyService
{
    public interface IPageCompanyService
    {
        Task<ApiResponse<object?>> PageCompanyDeleteCreate(PageCompanyDeleteCreateRequestModel Request);
        Task<ApiResponse<List<PageListResponseModel>>> PageCompanyList(int CompanyID);
    }
}