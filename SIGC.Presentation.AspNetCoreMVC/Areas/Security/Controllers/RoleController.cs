using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.RoleService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Controllers
{
    [Area("Security")]
    public class RoleController : BaseController
    {
        private readonly IRoleService RoleService;
        public RoleController(IRoleService RoleService)
        {
            this.RoleService = RoleService;
        }
        public IActionResult Index(){           
            return View("RoleIndex");
        }

        [HttpPost(Name = "RoleDataTable")]
        public async Task<IActionResult> RoleDataTable(DataTableHelper DataTable)
        {
            var ApiResponse = await RoleService.RolePagination(new RolePaginationRequestModel
            {
                CompanyID = 1,
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

        [HttpPost(Name = "RoleChangeState")]
        public async Task<IActionResult> RoleChangeState([FromBody] RoleChangeStateRequestModel Request)
        {            
            var ApiResponse =  await RoleService.RoleChangeState(Request);
            return Json(ApiResponse);
        }

    }
}
