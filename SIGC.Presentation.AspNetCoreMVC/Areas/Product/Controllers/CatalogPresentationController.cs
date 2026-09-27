using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPresentation;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogPresentationService;
using SIGC.Presentation.AspNetCoreMVC.Helpers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Controllers
{
    [Area("Product")]
    public class CatalogPresentationController : Controller
    {
        private readonly ICatalogPresentationService CatalogPresentationService;
        public CatalogPresentationController(ICatalogPresentationService CatalogPresentationService)
        {
            this.CatalogPresentationService = CatalogPresentationService;
        }

        [HttpPost]
        public async Task<IActionResult> CatalogPresentationCreate([FromBody] CatalogPresentationCreateRequestModel Request)
        {
            Request.RecordOriginID = (byte)EnumsHelper.RecordOrigin.WebForm;
            return Json(await CatalogPresentationService.CatalogPresentationCreate(Request));
        }

        [HttpGet]
        public async Task<IActionResult> CatalogPresentationList([FromRoute(Name = "id")] int CatalogID)
        {
            return Json(await CatalogPresentationService.CatalogPresentationList(CatalogID));
        }
    }
}
