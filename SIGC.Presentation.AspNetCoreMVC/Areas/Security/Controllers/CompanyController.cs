using Microsoft.AspNetCore.Mvc;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Controllers
{
    [Area("Security")]
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View("CompanyIndex");
        }
    }
}
