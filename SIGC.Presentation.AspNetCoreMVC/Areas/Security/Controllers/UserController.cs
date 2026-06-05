using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.User;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.UserCompany;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.CompanyService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UserCompanyService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UserService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Controllers
{
    [Area("Security")]
    public class UserController : BaseController
    { 
        private readonly IUserService UserService;
        private readonly IUserCompanyService UserCompanyService;
        private readonly ICompanyService CompanyService;
        public UserController(
             IUserService UserService,
             IUserCompanyService UserCompanyService,
             ICompanyService CompanyService
         )
        {
            this.UserService = UserService;
            this.UserCompanyService = UserCompanyService;
            this.CompanyService = CompanyService;
        }

        public IActionResult Index()
        {
            return View("UserIndex");
        }
        public async Task<IActionResult> Company()
        {
            ViewBag.CompanyList = (await CompanyService.CompanyList(GetSession().CompanyID)).Data;
            return View("UserIndex");
        }

        [HttpPost]
        public async Task<IActionResult> UserCreate([FromForm] UserCreateUpdateRequestModel Request)
        {
            if (Request.CompanyID == 0) Request.CompanyID = GetSession().CompanyID;
            return Json(await UserService.UserCreate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> UserUpdate([FromForm] UserCreateUpdateRequestModel Request)
        {
            if (Request.CompanyID == 0) Request.CompanyID = GetSession().CompanyID;
            return Json(await UserService.UserUpdate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> UserCompanyChangeState([FromBody] UserCompanyChangeStateRequestModel Request)
        {
            if (Request.CompanyID == 0) Request.CompanyID = GetSession().CompanyID;
            return Json(await UserCompanyService.UserCompanyChangeState(Request));
        }

        [HttpGet]
        [Route("Security/User/UserCompanyGet/{UserID}/{CompanyID?}")]
        public async Task<IActionResult> UserCompanyGet([FromRoute] int UserID, [FromRoute] int? CompanyID)
        {
            if(CompanyID.HasValue==false) CompanyID = GetSession().CompanyID;
            return Json(await UserCompanyService.UserCompanyGet(UserID, CompanyID.Value));
        }

        [HttpPost]
        public async Task<IActionResult> UserDataTable(UserPaginationRequestModel DataTable)
        {
            if (DataTable.CompanyID == 0) DataTable.CompanyID = GetSession().CompanyID;
                DataTable.PageNumber = (DataTable.iDisplayStart / DataTable.iDisplayLength) + 1;
                DataTable.PageSize = DataTable.iDisplayLength;
                DataTable.Search = DataTable.sSearch;

            var ApiResponse = await UserService.UserPagination(DataTable);         
            var Lista = ApiResponse.Data;
            var result = from sql in ApiResponse.Data.Items
                         select new[]{
                                 sql.UserID.ToString(),                              
                                 sql.UserLastName,
                                 sql.UserFirstName,
                                 sql.UserName,
                                 sql.UserRolNames,
                                 SpanStateType(sql.StateID),
                                 sql.UserLastUpdatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                                 sql.UserLastUpdatedUserName,
                                 sql.StateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUpdate}):"&nbsp",
                                 sql.StateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUnchange}):LinkHRef(new ControlModel{Value=PermissionModel.AccChange}),
                                 LinkHRef(new ControlModel{Value=PermissionModel.AccDelete})

             };
            return Json(new { sEcho = Convert.ToInt32(DataTable.sEcho), iTotalRecords = ApiResponse.Data.TotalRecords, iTotalDisplayRecords = ApiResponse.Data.RecordsFiltered, aaData = result });
        }
    }
}
