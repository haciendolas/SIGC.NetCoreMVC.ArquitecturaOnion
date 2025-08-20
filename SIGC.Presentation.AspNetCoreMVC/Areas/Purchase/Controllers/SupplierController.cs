using Microsoft.AspNetCore.Mvc;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Purchase.Controllers
{
    [Area("Purchase")]
    public class SupplierController : Controller
    {
        public IActionResult Index()
        {
            return View("SupplierIndex");
        }
    }
}
