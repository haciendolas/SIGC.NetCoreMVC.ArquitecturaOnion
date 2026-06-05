using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Controllers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Controllers
{
    [Area("Product")]
    public class CatalogController : BaseController
    {
        public IActionResult Index()
        {
            return View("CatalogIndex");
        }
    }
}
