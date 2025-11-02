using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Constant;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.ConstantService
{
    public interface IConstantService
    {
        Task<ApiResponse<List<ConstantListResponseModel>>> ConstantList(string ConstantClassConcat);       
    }
}