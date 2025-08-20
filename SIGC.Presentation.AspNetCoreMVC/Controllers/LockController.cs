using Microsoft.AspNetCore.Mvc;

namespace SIGC.Presentation.AspNetCoreMVC.Controllers
{
    public class LockController : Controller
    {
        public IActionResult LockScreen()
        {
            return PartialView("LockScreen");
        }        
    }
}
