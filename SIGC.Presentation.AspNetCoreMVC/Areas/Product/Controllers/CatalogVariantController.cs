using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogVariant;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogVariantService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Controllers
{
    [Area("Product")]
    public class CatalogVariantController : BaseController
    {
        private readonly ICatalogVariantService CatalogVariantService;
        public CatalogVariantController(ICatalogVariantService CatalogVariantService)
        {
            this.CatalogVariantService = CatalogVariantService;
        }

        [HttpPost]
        public async Task<IActionResult> CatalogVariantCreate([FromBody] CatalogVariantCreateUpdateRequestModel Request)
        {
            Request.RecordOriginID = (byte)EnumsHelper.RecordOrigin.WebForm;
            return Json(await CatalogVariantService.CatalogVariantCreate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> CatalogVariantUpdate([FromBody] CatalogVariantCreateUpdateRequestModel Request)
        {
            Request.RecordOriginID = (byte)EnumsHelper.RecordOrigin.WebForm;
            return Json(await CatalogVariantService.CatalogVariantUpdate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> CatalogVariantChangeState([FromBody] CatalogVariantChangeStateRequestModel Request)
        {
            return Json(await CatalogVariantService.CatalogVariantChangeState(Request));
        }

        [HttpGet]
        public async Task<IActionResult> CatalogVariantList([FromRoute(Name = "id")] int CatalogID)
        {
            return Json(await CatalogVariantService.CatalogVariantList(CatalogID));
        }

        [HttpGet]
        public async Task<IActionResult> CatalogVariantListActive([FromRoute(Name = "id")] int CatalogID)
        {
            var ApiResponse = await CatalogVariantService.CatalogVariantList(CatalogID);
            var CatalogVariantList = ApiResponse.Data.Where(w => w.CatalogVariantStateID == (byte)EnumsHelper.StateType.Active)
                .Select(sel=>new OptionResultModel
                {
                    Value = sel.CatalogVariantID,
                    Label = sel.CatalogVariantName
                }).ToList();
            return Json(new ApiResponse<List<OptionResultModel>>
            {
                Data = CatalogVariantList,
                Message = ApiResponse.Message,
                Type   =ApiResponse.Type
            });
        }
    }
}
