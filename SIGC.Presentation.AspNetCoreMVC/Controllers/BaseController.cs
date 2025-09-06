using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Extensions;
using SIGC.Presentation.AspNetCoreMVC.Filters;
using SIGC.Presentation.AspNetCoreMVC.Helpers;

namespace SIGC.Presentation.AspNetCoreMVC.Controllers
{
    public class BaseController : Controller
    {
        public AuthenticationIdentity GetSession()
        {            
            return HttpContext.Session.GetObject<AuthenticationIdentity>(ConstantsHelper.SessionKeys.AuthenticationIdentity) ?? new AuthenticationIdentity();
        }      
    }
}