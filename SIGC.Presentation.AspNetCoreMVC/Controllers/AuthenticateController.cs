
using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Extensions;
using SIGC.Presentation.AspNetCoreMVC.Filters;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models.Auth;
using SIGC.Presentation.AspNetCoreMVC.Services.AuthService;

namespace SIGC.Presentation.AspNetCoreMVC.Controllers
{
    public class AuthenticateController : BaseController
    {       
        private readonly IAuthService AuthService;
       
        public AuthenticateController(IAuthService AuthService)
        {
            this.AuthService = AuthService;           
        }
       
        [HttpPost]
        public async Task<ActionResult> Authenticate(AuthLoginTokenRequestModel model){
            string Url = string.Empty;
            string Message = string.Empty;
            var ApiResponse = await AuthService.SignIn(model);

            // var response = UserList().FirstOrDefault(f => f.UserPassword == model.UserPassword && f.UserName == model.UserName && f.CompanyDocumentNumber == model.CompanyDocumentNumber);
            if (ApiResponse.Data is not null)        {
                Url = "Dashboard";

                var authenticationIdentity = ConvertsHelper.ExtractUserInfo(ApiResponse.Data.Value.AccessToken); 

                if (HttpContext.Session != null)
                {
                    if (HttpContext.Session.GetObject<AuthenticationIdentity>(ConstantsHelper.SessionKeys.AuthenticationIdentity) == null)
                    {
                        HttpContext.Session.SetObject(ConstantsHelper.SessionKeys.AuthenticationIdentity, authenticationIdentity);
                    }
                    if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ConstantsHelper.SessionKeys.AccessToken)))
                    {
                        HttpContext.Session.SetString(ConstantsHelper.SessionKeys.AccessToken, ApiResponse.Data.Value.AccessToken);
                    }
                    if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ConstantsHelper.SessionKeys.RefreshToken)))
                    {
                        HttpContext.Session.SetString(ConstantsHelper.SessionKeys.RefreshToken, ApiResponse.Data.Value.RefreshToken);
                    }
                }               
            }       
            return Ok(new { Message=ApiResponse.Message, Url = Url });
        }
        /*
        [HttpGet]
        public ActionResult ExpiredSession() {
            var msgResponse = new MsgResponse<string>();            
            msgResponse.Type = MsgOperation.MessageType.Session.ToString();
            msgResponse.Title = MsgOperation.GetEnumDescription(MsgOperation.MessageTitle.AssistantSession);
            msgResponse.Message = MsgOperation.GetEnumDescription(MsgOperation.MessageDescription.VerifyExpiredSession);
            msgResponse.Session = false;
            msgResponse.Function = "Uti.Modal.Session()";
            return Json(msgResponse);
        }
        */

    }
}
