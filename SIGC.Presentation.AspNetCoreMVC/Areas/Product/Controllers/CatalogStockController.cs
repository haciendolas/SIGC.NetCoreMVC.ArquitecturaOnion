using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogStock;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogStockService;
using SIGC.Presentation.AspNetCoreMVC.Helpers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Controllers
{
    [Area("Product")]
    public class CatalogStockController : Controller
    {
        private readonly ICatalogStockService CatalogStockService;
        public CatalogStockController(ICatalogStockService CatalogStockService)
        {
            this.CatalogStockService = CatalogStockService;
        }
     
        [HttpPost]
        public async Task<IActionResult> CatalogStockCreate([FromBody] CatalogStockCreateUpdateRequestModel Request)
        {
            Request.RecordOriginID = (byte)EnumsHelper.RecordOrigin.WebForm;
            Request.CatalogStockCurrentQuantity = 0;
            return Json(await CatalogStockService.CatalogStockCreate(Request));
        }
       /*
        [HttpPut]
        public async Task<IActionResult> CatalogStockUpdate([FromBody] CatalogStockCreateUpdateRequestModel Request)
        { 
            return Json(await CatalogStockService.CatalogStockUpdate(Request));
        }
      
        [HttpPut]
        public async Task<IActionResult> CatalogStockChangeState([FromBody] CatalogStockChangeStateRequestModel Request)
        {
            return Json(await CatalogStockService.CatalogStockChangeState(Request));
        }
 
        [HttpGet]
        public async Task<IActionResult> CatalogStockList([FromRoute(Name = "id")] int CatalogID)
        {
            return Json(await CatalogStockService.CatalogStockList(CatalogID));
        }
       */
    }
}
