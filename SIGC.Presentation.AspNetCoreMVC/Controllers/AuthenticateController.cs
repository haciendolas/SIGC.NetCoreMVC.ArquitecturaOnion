
using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Extensions;
using SIGC.Presentation.AspNetCoreMVC.Filters;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models.Auth;
using SIGC.Presentation.AspNetCoreMVC.Services.AuthService;
using SIGC.Presentation.AspNetCoreMVC.Services.EstablishmentService;
using SIGC.Presentation.AspNetCoreMVC.Services.RolePermissionService;

namespace SIGC.Presentation.AspNetCoreMVC.Controllers
{
    public class AuthenticateController : BaseController
    {       
        private readonly IAuthService AuthService;
        private readonly IRolePermissionService RolePermissionService;
        private readonly IEstablishmentService EstablishmentService;
        public AuthenticateController(IAuthService AuthService,
            IRolePermissionService RolePermissionService,
            IEstablishmentService EstablishmentService)
        {
            this.AuthService = AuthService;
            this.RolePermissionService = RolePermissionService;
            this.EstablishmentService = EstablishmentService;
        }
       
        [HttpPost]
        public async Task<ActionResult> Authenticate(AuthLoginTokenRequestModel model){
            string Url = string.Empty;
            string Message = string.Empty;
            var ApiResponse = await AuthService.SignIn(model);
                       
            if (ApiResponse.Data is not null)        {
                Url = "Dashboard";

                var AuthenticationIdentity = ConvertsHelper.ExtractUserInfo(ApiResponse.Data.Value.AccessToken);
                    AuthenticationIdentity.UserImage = ApiResponse.Data.Value.AccountInfo.UserPhotoUrl;
                    AuthenticationIdentity.UserLastName = ApiResponse.Data.Value.AccountInfo.UserLastName;
                    AuthenticationIdentity.UserFirstName = ApiResponse.Data.Value.AccountInfo.UserFirstName;
                    AuthenticationIdentity.UserFullName = $"{ApiResponse.Data.Value.AccountInfo.UserFirstName} {ApiResponse.Data.Value.AccountInfo.UserLastName}";
                if (HttpContext.Session != null)
                {
                    if (HttpContext.Session.GetObject<AuthenticationIdentity>(ConstantsHelper.SessionKeys.AuthenticationIdentity) == null)
                    {
                        HttpContext.Session.SetObject(ConstantsHelper.SessionKeys.AuthenticationIdentity, AuthenticationIdentity);
                    }
                    if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ConstantsHelper.SessionKeys.AccessToken)))
                    {
                        HttpContext.Session.SetString(ConstantsHelper.SessionKeys.AccessToken, ApiResponse.Data.Value.AccessToken);
                    }
                    if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ConstantsHelper.SessionKeys.RefreshToken)))
                    {
                        HttpContext.Session.SetString(ConstantsHelper.SessionKeys.RefreshToken, ApiResponse.Data.Value.RefreshToken);
                    }
                    if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ConstantsHelper.SessionKeys.MenuSidebar)))
                    {
                        var ApiResponseRolePermission = await RolePermissionService.RolePermissionList(new Models.RolePermission.RolePermissionListRequestModel
                        {
                            UserID = AuthenticationIdentity.UserID,
                            CompanyID = AuthenticationIdentity.CompanyID
                        });
                        HttpContext.Session.SetObject(ConstantsHelper.SessionKeys.MenuSidebar, ApiResponseRolePermission.Data!);
                    }
                    if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(ConstantsHelper.SessionKeys.Establishment)))
                    {
                        var ApiResponseEstablishment = await EstablishmentService.EstablishmentList(AuthenticationIdentity.CompanyID);
                        HttpContext.Session.SetObject(ConstantsHelper.SessionKeys.Establishment, ApiResponseEstablishment.Data!);
                    }
                }               
            }       
            return Json(new { Message=ApiResponse.Message, Url = Url });
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
