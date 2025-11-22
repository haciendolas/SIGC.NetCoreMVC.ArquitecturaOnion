using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Page;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageCompanyService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.PageService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Services;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Controllers
{
    [Area("Security")]
    public class PageController : BaseController
    {

        private readonly IPageService PageService;
        private readonly IPageCompanyService PageCompanyService;

        public PageController(IPageService PageService, IPageCompanyService PageCompanyService)
        {           
            this.PageService = PageService;
            this.PageCompanyService = PageCompanyService;        
        }

        [HttpGet]
        //public async Task<IActionResult> PageTreeViewWithAction([FromRoute] int? id) 
        public async Task<IActionResult> PageTreeViewWithAction([FromRoute(Name = "id")] int? CompanyID)
        {
            var ApiResponsePage = new ApiResponse<List<PageListResponseModel>>();
            if (CompanyID.HasValue == false)
            {
                CompanyID = GetSession().CompanyID;
                ApiResponsePage = await PageCompanyService.PageCompanyList(CompanyID.Value);
            }
            else
            {
                ApiResponsePage = await PageService.PageList();
            }
            var ApiResponse = new ApiResponse<string>();
            ApiResponse.Type = ApiResponsePage.Type;
            ApiResponse.Data = this.TreeViewWithAction(ApiResponsePage.Data!, 0);
            return Json(ApiResponse);
        }

        private string TreeViewWithAction(List<PageListResponseModel> Pages, int PageParentID)
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
                            MyUL += "<label id=lblPageID_" + item.PageID + " name=lblPageID >" + item.PageName + "</label>";
                            MyUL += "</div>";
                            MyUL += this.TreeViewWithAction(Pages, item.PageID);
                        }
                        else
                        {
                            if (item.PageAction.Any())
                            {
                                MyUL += "<div>";
                                MyUL += "<input type=hidden id=chkPageID_" + item.PageID + " name=chkPageID value=" + item.PageID + " />";
                                MyUL += "<label for=chkPageID_" + item.PageID + " id=lblPageID_" + item.PageID + " name=lblPageID />" + item.PageName + "</label>";
                                MyUL += "</div>";
                                MyUL += "<div id=" + item.PageID + ">";
                                foreach (var Action in item.PageAction)
                                {
                                    MyUL += "<div class='form-check form-check-secondary mb-2'>";
                                    MyUL += "<input class='form-check-input' type=checkbox id=chkPageActionID_" + Action.PageActionID + " name=chkPageActionID value=" + Action.PageActionID + " style='width:23px;height:23px'/>";
                                    MyUL += "<label class='form-check-label p-1' for=chkPageActionID_" + Action.PageActionID + " id=lblPageActionID_" + Action.PageActionID + " name=lblPageActionID>" + Action.PageActionDescription + "</label>";
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

        [HttpGet] 
        public async Task<IActionResult> PageTreeView()
        { 
            var ApiResponsePage = await PageService.PageList();         
            var ApiResponse = new ApiResponse<string>();
            ApiResponse.Type = ApiResponsePage.Type;
            ApiResponse.Data = this.TreeView(ApiResponsePage.Data!, 0);
            return Json(ApiResponse);
        }

        private string TreeView(List<PageListResponseModel> Pages, int PageParentID)
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
                            MyUL += "<label id=lblPageID_" + item.PageID + " name=lblPageID >" + item.PageName + "</label>";
                            MyUL += "</div>";
                            MyUL += this.TreeView(Pages, item.PageID);
                        }
                        else{                           
                                MyUL += "<div class='form-check form-check-secondary mb-2'>";
                                MyUL += "<input type=checkbox class='form-check-input' id=chkPageID_" + item.PageID + " name=chkPageID  value=" + item.PageID + " style='width:23px;height:23px' />";
                                MyUL += "<label class='form-check-label p-1' for=chkPageID_" + item.PageID + " id=lblPageID_" + item.PageID + " name=lblPageID />" + item.PageName + "</label>";
                                MyUL += "</div>";                           
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
