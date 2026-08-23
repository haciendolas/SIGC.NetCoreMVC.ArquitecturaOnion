using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Attribute;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.AttributeService
{
    public interface IAttributeService
    {
        Task<ApiResponse<List<AttributeListResponseModel>>> AttributeValueList(bool? AttributeIsVariant = null);
    }
}