
using SIGC.Presentation.AspNetCoreMVC.Areas.Accounting.Models.Tax;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Accounting.Services.TaxService
{
    public interface ITaxService
    {
        Task<ApiResponse<List<TaxListResponseModel>>> TaxList();
    }
}