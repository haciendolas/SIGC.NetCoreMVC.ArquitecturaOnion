using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Ubigeo;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UbigeoService
{
    public interface IUbigeoService
    {
        Task<ApiResponse<List<UbigeoListSearchResponsetModel>>> UbigeoListSearch(UbigeoListSearchRequestModel Request);
        Task<ApiResponse<List<UbigeoListByUbigeoClassResponseModel>>> UbigeoListByUbigeoClass(int UbigeoClass);
        Task<ApiResponse<List<UbigeoListByClassAndCodeAndLenCodeResponseModel>>> UbigeoListByClassAndCodeAndLenCode(UbigeoListByClassAndCodeAndLenCodeRequestModel Request);
    }
}