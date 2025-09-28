using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Page;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageCompanyService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.RoleService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using SIGC.Presentation.AspNetCoreMVC.Services;
using System.Collections.Generic;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Controllers
{
    [Area("Security")]
    public class RoleController : BaseController
    {
        private readonly IRoleService RoleService;
        private readonly IPageService PageService;
        private readonly IPageCompanyService PageCompanyService;
        public RoleController(IRoleService RoleService, IPageService PageService, IPageCompanyService PageCompanyService)
        {
            this.RoleService = RoleService;
            this.PageService = PageService;
            this.PageCompanyService = PageCompanyService;
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

        [HttpGet]
        public async Task<IActionResult> PageList([FromQuery]int? CompanyID)
        {
            var ApiResponsePage = new ApiResponse<List<PageListResponseModel>>();
            if (CompanyID.HasValue)
            {
                CompanyID = GetSession().CompanyID;
                ApiResponsePage = await PageCompanyService.PageCompanyList(CompanyID.Value);              
            }
            else{
                ApiResponsePage = await PageService.PageList();
            }
            
            var ApiResponse = new ApiResponse<string>();
            ApiResponse.Type = ApiResponsePage.Type;
            ApiResponse.Data = this.PageTreeView(ApiResponsePage.Data!, 0);
            return Json(ApiResponse);
        }

        private string PageTreeView(List<PageListResponseModel> Pages, int PageParentID)
        {
            string MyUL = "";
            if (Pages.Any())
            {
                var List = Pages.Where(w => w.PageParentID == PageParentID).OrderBy(ord => ord.PageOrder).ToList();
                if (List.Any())
                {
                    MyUL = "<ul>";
                    foreach (var item in List)
                    {
                        MyUL += "<li>";
                        var SubList = Pages.Where(w => w.PageParentID == item.PageID).ToList();
                        if (SubList.Any())
                        {
                            MyUL += "<label>" + item.PageName + "</label>";
                            MyUL += this.PageTreeView(Pages, item.PageID);
                        }
                        else
                        {
                            if (item.PageAction.Any())
                            {
                                MyUL += "<input type=checkbox />" + item.PageName;
                                MyUL += "<div class='form-check pages' id="+item.PageID+">";
                                foreach (var Action in item.PageAction)
                                {
                                    MyUL += "<input type=checkbox />" + Action.PageActionDescription+"<br/>";
                                }
                                MyUL += "</div>";
                            }
                            else
                            {
                                MyUL+="<input type=checkbox />"+item.PageName;
                            }
                        }
                        MyUL += "</li>";
                    }
                    MyUL += "</ul>";
                }
            }
            return MyUL;
        }

    }
}
