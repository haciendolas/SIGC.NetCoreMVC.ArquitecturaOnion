using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Page;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageCompanyService
{
    public interface IPageCompanyService
    {
        Task<ApiResponse<List<PageListResponseModel>>> PageCompanyList(int CompanyID);
    }
}
