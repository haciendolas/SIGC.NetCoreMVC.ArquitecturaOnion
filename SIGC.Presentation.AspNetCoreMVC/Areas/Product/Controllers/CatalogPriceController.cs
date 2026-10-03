using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPrice; 
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogPriceService;
using SIGC.Presentation.AspNetCoreMVC.Helpers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Controllers
{
    [Area("Product")]
    public class CatalogPriceController : Controller
    {
        private readonly ICatalogPriceService CatalogPriceService;
        public CatalogPriceController(ICatalogPriceService CatalogPriceService)
        {
            this.CatalogPriceService = CatalogPriceService;
        }

        [HttpPost]
        public async Task<IActionResult> CatalogPriceCreate([FromBody] CatalogPriceCreateUpdateRequestModel Request)
        {
            Request.RecordOriginID = (byte)EnumsHelper.RecordOrigin.WebForm;
            return Json(await CatalogPriceService.CatalogPriceCreate(Request));
        }
        /*
        [HttpPut]
        public async Task<IActionResult> CatalogPriceChangeState([FromBody] CatalogPriceChangeStateRequestModel Request)
        {
            return Json(await CatalogPriceService.CatalogPriceChangeState(Request));
        }

        [HttpGet]
        public async Task<IActionResult> CatalogPriceList([FromRoute(Name = "id")] int CatalogID)
        {
            return Json(await CatalogPriceService.CatalogPriceList(CatalogID));
        }
        */
    }
}
