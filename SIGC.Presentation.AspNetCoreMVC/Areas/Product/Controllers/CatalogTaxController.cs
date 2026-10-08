using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTaxService;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Controllers
{
    [Area("Product")]
    public class CatalogTaxController : Controller
    {
        private readonly ICatalogTaxService CatalogTaxService;
        public CatalogTaxController(ICatalogTaxService CatalogTaxService)
        {
            this.CatalogTaxService = CatalogTaxService;
        }
        /*
        [HttpPost]
        public async Task<IActionResult> CatalogTaxCreate([FromBody] CatalogTaxCreateUpdateRequestModel Request)
        {
            Request.RecordOriginID = (byte)EnumsHelper.RecordOrigin.WebForm;
            return Json(await CatalogTaxService.CatalogTaxCreate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> CatalogTaxUpdate([FromBody] CatalogTaxCreateUpdateRequestModel Request)
        { 
            return Json(await CatalogTaxService.CatalogTaxUpdate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> CatalogTaxChangeState([FromBody] CatalogTaxChangeStateRequestModel Request)
        {
            return Json(await CatalogTaxService.CatalogTaxChangeState(Request));
        }

        [HttpDelete]
        public async Task<IActionResult> CatalogTaxDelete([FromRoute(Name = "id")] int CatalogTaxID)
        {
            return Json(await CatalogTaxService.CatalogTaxDelete(CatalogTaxID));
        }
        */
        [HttpGet]
        public async Task<IActionResult> CatalogTaxList([FromRoute(Name = "id")] int CatalogID)
        {
            return Json(await CatalogTaxService.CatalogTaxList(CatalogID));
        }
       
    }
}
