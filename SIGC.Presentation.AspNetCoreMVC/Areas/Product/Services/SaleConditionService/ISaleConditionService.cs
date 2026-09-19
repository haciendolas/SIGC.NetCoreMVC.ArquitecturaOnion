using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.SaleCondition;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.SaleConditionService
{
    public interface ISaleConditionService
    {
        Task<ApiResponse<List<SaleConditionListResponseModel>>> SaleConditionList();
    }
}