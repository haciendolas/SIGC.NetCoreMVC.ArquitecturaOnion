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

        [HttpPost]
        public async Task<IActionResult> RoleCreate([FromBody] RoleCreateUpdateRequestModel Request)
        {
            var ApiResponse = await RoleService.RoleCreate(Request);
            return Json(ApiResponse);
        }

        [HttpGet]
       //public async Task<IActionResult> PageList([FromRoute] int? id) 
       public async Task<IActionResult> PageList([FromRoute(Name = "id")] int? CompanyID)
        {
            var ApiResponsePage = new ApiResponse<List<PageListResponseModel>>();
            if (CompanyID.HasValue)
            {  
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
                            MyUL += "<div>";                          
                            MyUL += "<label id=lblPageID_" + item.PageID+" name=lblPageID >" + item.PageName + "</label>";
                            MyUL += "</div>";
                            MyUL += this.PageTreeView(Pages, item.PageID);
                        }
                        else{
                            if (item.PageAction.Any())
                            {
                                MyUL += "<div>";
                                MyUL += "<input type=hidden id=chkPageID_"+item.PageID+ " name=chkPageID value="+item.PageID+" />";
                                MyUL += "<label for=chkPageID_" + item.PageID+ " id=lblPageID_"+item.PageID+ " name=lblPageID />" + item.PageName + "</label>";
                                MyUL += "</div>"; 
                                MyUL += "<div id="+item.PageID+">";
                                foreach (var Action in item.PageAction)
                                {
                                    MyUL += "<div class='form-check form-check-secondary mb-2'>";
                                    MyUL += "<input class='form-check-input' type=checkbox id=chkPageActionID_"+Action.PageActionID+" name=chkPageActionID value="+Action.PageActionID+" style='width:23px;height:23px'/>";
                                    MyUL += "<label class='form-check-label p-1' for=chkPageActionID_" + Action.PageActionID+" id=lblPageActionID_"+Action.PageActionID+" name=lblPageActionID>" + Action.PageActionDescription+"</label>";
                                    MyUL += "</div>";
                                }
                                MyUL += "</div>";
                            }
                            else
                            {
                                MyUL += "<div class='form-check form-check-secondary mb-2'>";
                                MyUL += "<input type=checkbox class='form-check-input' id=chkPageID_" + item.PageID + " name=chkPageID  value=" + item.PageID + " style='width:23px;height:23px' />";
                                MyUL += "<label class='form-check-label p-1' for=chkPageID_" + item.PageID + " id=lblPageID_" + item.PageID + " name=lblPageID />" + item.PageName + "</label>";
                                MyUL += "</div>";
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
