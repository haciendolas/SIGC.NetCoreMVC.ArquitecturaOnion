using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogPresentationService;

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

        [HttpGet]
        public async Task<IActionResult> CatalogPresentationList([FromRoute(Name = "id")] int CatalogID)
        {
            return Json(await CatalogPresentationService.CatalogPresentationList(CatalogID));
        }
    }
}
