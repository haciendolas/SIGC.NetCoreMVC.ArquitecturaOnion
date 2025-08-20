 
using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Extensions;
using SIGC.Presentation.AspNetCoreMVC.Filters;
using SIGC.Presentation.AspNetCoreMVC.Models;

namespace SIGC.Presentation.AspNetCoreMVC.Controllers
{
    public class AuthenticateController : BaseController
    {
        /*
        private readonly ILoginAppService _loginAppService;
        private readonly IPeriodAppService _periodAppService;
        public AuthenticateController(ILoginAppService loginAppService, IPeriodAppService periodAppService = null)
        {
            _loginAppService = loginAppService;
            _periodAppService = periodAppService;
        }
        */
        [HttpPost]
        public async Task<ActionResult> Authenticate(LoginRequestModel model){
            string url = string.Empty;
            string Message = string.Empty;
           // var baseResponse = await _loginAppService.SignIn(model);
           // LoginResponseModel User = baseResponse.Data!;
            
                url = "Dashboard";
            

            var response = UserList().FirstOrDefault(f => f.UserPassword == model.UserPassword && f.UserName == model.UserName && f.CompanyDocumentNumber == model.CompanyDocumentNumber);
            if (response is null) Message= "Usuario y clave incorrecto";
                Message = "Bienvenido al sistema";

            var User = new LoginResponseModel();
            User.UserID = response.UserId;
            User.UserName = response.UserName;
            User.CompanyDocumentNumber = response.CompanyDocumentNumber;

            var authenticationIdentity = GetAuthenticationIdentity(User);
            if (HttpContext.Session != null){
                   if (HttpContext.Session.GetObject<AuthenticationIdentity>("AuthenticationIdentity") == null){
                     HttpContext.Session.SetObject("AuthenticationIdentity", authenticationIdentity);
                  }
                }                
                          
            return Ok(new { Message, Url = url });
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

        private List<UserResponseDto> UserList()
        {
            var list = new List<UserResponseDto>() {
               new UserResponseDto
               {
                   UserId=1,
                   CompanyId=1,
                   CompanyDocumentNumber="10404358087",
                   UserName="Administrador",
                   UserPassword="123456"
               },
               new UserResponseDto
               {
                   UserId=2,
                   CompanyId=1,
                   CompanyDocumentNumber="10404358086",
                   UserName="jcastillo",
                   UserPassword="123456"
               }
            };

            return list;
        }
    }
}
