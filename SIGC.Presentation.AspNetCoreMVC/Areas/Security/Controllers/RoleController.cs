using Microsoft.AspNetCore.Mvc;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Controllers
{
    [Area("Security")]
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View("RoleIndex");
        }
    }
}
