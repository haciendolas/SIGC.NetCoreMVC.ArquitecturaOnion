using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PresentationService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Controllers
{
    [Area("Product")]
    public class PresentationController : BaseController
    {
        private readonly IPresentationService PresentationService;
        public PresentationController(IPresentationService PresentationService)
        {
            this.PresentationService = PresentationService;
        }

        public IActionResult Index()
        {
            return View("PresentationIndex");
        }

        [HttpGet]
        public async Task<IActionResult> PresentationList([FromRoute(Name = "id")] int UnitMeasureID)
        {
            return Json(await PresentationService.PresentationList(UnitMeasureID));
        }
    }
}
