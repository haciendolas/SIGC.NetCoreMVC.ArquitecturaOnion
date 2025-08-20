using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Extensions;
using SIGC.Presentation.AspNetCoreMVC.Filters;
using SIGC.Presentation.AspNetCoreMVC.Models;

namespace SIGC.Presentation.AspNetCoreMVC.Controllers
{
    public class BaseController : Controller
    {
        public AuthenticationIdentity GetSession()
        {            
            return HttpContext.Session.GetObject<AuthenticationIdentity>("AuthenticationIdentity") ?? new AuthenticationIdentity();
        }

     
        public AuthenticationIdentity GetAuthenticationIdentity(LoginResponseModel User)
        {
            var authenticationIdentity = new AuthenticationIdentity();
            if (User != null){
                authenticationIdentity.UserID = User.UserID;
                authenticationIdentity.UserName = User.UserName;  
                authenticationIdentity.UserImage = User.UserImage; 
                authenticationIdentity.UserDocumentNumber = User.UserDocumentNumber;
                authenticationIdentity.CompanyDocumentNumber = User.CompanyDocumentNumber;

            }
            return authenticationIdentity;
        }
       
    }
}