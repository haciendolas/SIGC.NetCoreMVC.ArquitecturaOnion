using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogVariant;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogVariantService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;

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

        [HttpGet]
        public async Task<IActionResult> CatalogVariantList([FromRoute(Name = "id")] int CatalogID)
        {
            return Json(await CatalogVariantService.CatalogVariantList(CatalogID));
        }
    }
}
