using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.CompanyService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.RoleService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Controllers
{
    [Area("Security")]
    public class RoleController : BaseController
    {
        private readonly IRoleService RoleService;  
        private readonly ICompanyService CompanyService;
        public RoleController(IRoleService RoleService, IPageService PageService,ICompanyService CompanyService)
        {
            this.RoleService = RoleService; 
            this.CompanyService = CompanyService;
        }
        public IActionResult Index(){           
            return View("RoleIndex");
        }

        public async Task<IActionResult> Company()
        {
            ViewBag.CompanyList = (await CompanyService.CompanyList(GetSession().CompanyID)).Data;
            return View("RoleIndex");
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate([FromBody] RoleCreateUpdateRequestModel Request)
        {
            if (Request.CompanyID == 0) Request.CompanyID = GetSession().CompanyID;
            return Json(await RoleService.RoleCreate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> RoleUpdate([FromBody] RoleCreateUpdateRequestModel Request)
        {
            if (Request.CompanyID == 0) Request.CompanyID = GetSession().CompanyID;
            return Json(await RoleService.RoleUpdate(Request));
        }

        [HttpPost(Name = "RoleChangeState")]
        public async Task<IActionResult> RoleChangeState([FromBody] RoleChangeStateRequestModel Request)
        {
            if (Request.CompanyID == 0) Request.CompanyID = GetSession().CompanyID;
            return Json(await RoleService.RoleChangeState(Request));
        }

        [HttpGet]   
        public async Task<IActionResult> RoleGet([FromRoute(Name = "id")] int RoleID)
        {  
            return Json(await RoleService.RoleGet(RoleID));
        }

        [HttpPost(Name = "RoleDataTable")]
        public async Task<IActionResult> RoleDataTable(DataTableHelper DataTable)
        {
            if (DataTable.sCompanyID == 0) DataTable.sCompanyID = GetSession().CompanyID;
            var ApiResponse = await RoleService.RolePagination(new RolePaginationRequestModel
            {
                CompanyID = DataTable.sCompanyID,
                StateID = DataTable.sStateID,
                PageNumber = (DataTable.iDisplayStart / DataTable.iDisplayLength) + 1,
                PageSize = DataTable.iDisplayLength,
                Search = DataTable.sSearch ?? ""

            });
            var Lista = ApiResponse.Data;
            var result = from sql in ApiResponse.Data.Items
                         select new[]{
                                 sql.RoleID.ToString(),
                                 sql.RoleCode,
                                 sql.RoleName,
                                 SpanStateType(sql.StateID),
                                 sql.RoleLastUpdatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                                 sql.RoleLastUpdatedUserName,
                                 sql.StateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUpdate}):"&nbsp:",
                                 sql.StateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUnchange}):LinkHRef(new ControlModel{Value=PermissionModel.AccChange}),
                                 LinkHRef(new ControlModel{Value=PermissionModel.AccDelete})

             };
            return Json(new { sEcho = Convert.ToInt32(DataTable.sEcho), iTotalRecords = ApiResponse.Data.TotalRecords, iTotalDisplayRecords = ApiResponse.Data.RecordsFiltered, aaData = result });
        } 
    }
}
