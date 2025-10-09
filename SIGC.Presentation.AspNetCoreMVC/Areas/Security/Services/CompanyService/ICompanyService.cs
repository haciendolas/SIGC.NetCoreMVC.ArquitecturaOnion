using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Company;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.CompanyService
{
    public interface ICompanyService
    {
        Task<ApiResponse<List<CompanyListResponseModel>>> CompanyList(int CompanyIDRegister);
    }
}
