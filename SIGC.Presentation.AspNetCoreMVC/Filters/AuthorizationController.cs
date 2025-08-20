 
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Extensions;

namespace SIGC.Presentation.AspNetCoreMVC.Filters
{
    public class AuthorizationController : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext){
            if (filterContext.HttpContext.Session.GetObject<AuthenticationIdentity>("AuthenticationIdentity") == null){
                
                string NameController = filterContext.RouteData.Values["controller"].ToString();
                if (filterContext.HttpContext.Request.Method == "GET" && NameController != "Lock")
                {
                    if (filterContext.Controller is AuthenticateController == false)
                    {
                        if (filterContext.Controller is LoginController == false)
                        {
                            filterContext.HttpContext.Response.Redirect("/Login/Index");
                        }
                    }
                }
                else
                {
                    if (filterContext.HttpContext.Request.Method == "POST" && NameController != "Authenticate")
                    {
                        string SegmentoUrl = filterContext.HttpContext.Request.GetEncodedPathAndQuery().Substring(1, filterContext.HttpContext.Request.GetEncodedPathAndQuery().ToString().Length - 1);
                        if (SegmentoUrl.Contains("/"))
                        {
                            SegmentoUrl = SegmentoUrl.Split('/')[0];
                        }
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                              { "Controller",SegmentoUrl==NameController ? "Authenticate" :"../Authenticate"},
                              { "Action", "ExpiredSession" }
                        });
                    }
                }
            }
            else
            {
                if (filterContext.Controller is LoginController == true)
                {
                    filterContext.HttpContext.Response.Redirect("/Dashboard");
                }
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
