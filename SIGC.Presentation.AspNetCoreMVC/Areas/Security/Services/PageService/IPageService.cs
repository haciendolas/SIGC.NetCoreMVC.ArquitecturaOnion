using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Page;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageService
{
    public interface IPageService
    {
        Task<ApiResponse<List<PageListResponseModel>>> PageList();
    }
}
