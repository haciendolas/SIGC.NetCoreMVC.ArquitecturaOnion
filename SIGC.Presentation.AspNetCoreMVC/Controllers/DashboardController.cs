using Microsoft.AspNetCore.Mvc;

namespace SIGC.Presentation.AspNetCoreMVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
