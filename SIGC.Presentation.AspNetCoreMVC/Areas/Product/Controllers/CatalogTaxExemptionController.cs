using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTaxExemption;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTaxExemptionService;
using SIGC.Presentation.AspNetCoreMVC.Helpers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Controllers
{
    [Area("Product")]
    public class CatalogTaxExemptionController : Controller
    {
        private readonly ICatalogTaxExemptionService CatalogTaxExemptionService;
        public CatalogTaxExemptionController(ICatalogTaxExemptionService CatalogTaxExemptionService)
        {
            this.CatalogTaxExemptionService = CatalogTaxExemptionService;
        }
     
        [HttpPost]
        public async Task<IActionResult> CatalogTaxExemptionCreate([FromBody] CatalogTaxExemptionCreateUpdateRequestModel Request)
        {
            Request.RecordOriginID = (byte)EnumsHelper.RecordOrigin.WebForm;
            return Json(await CatalogTaxExemptionService.CatalogTaxExemptionCreate(Request));
        }
        /*
          [HttpDelete]
          public async Task<IActionResult> CatalogTaxExemptionDelete([FromRoute(Name = "id")] int CatalogTaxExemptionID)
          {
              return Json(await CatalogTaxExemptionService.CatalogTaxExemptionDelete(CatalogTaxExemptionID));
          }
         
        [HttpGet]
        public async Task<IActionResult> CatalogTaxExemptionList([FromRoute(Name = "id")] int CatalogID)
        {
            return Json(await CatalogTaxExemptionService.CatalogTaxExemptionList(CatalogID));
        }
        */
    }
}
