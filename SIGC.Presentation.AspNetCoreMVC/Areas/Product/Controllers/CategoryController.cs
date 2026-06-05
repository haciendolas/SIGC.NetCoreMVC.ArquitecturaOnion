using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CategoryService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Controllers
{
    [Area("Product")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService CategoryService;
        public CategoryController(ICategoryService CategoryService) { 
            this.CategoryService = CategoryService;
        }

        public IActionResult Index()
        {
            return View("CategoryIndex");
        }

        [HttpPost]
        public async Task<IActionResult> CategoryCreate([FromForm] CategoryCreateUpdateRequestModel Request)
        {
            Request.RecordOriginId = (byte)EnumsHelper.RecordOrigin.WebForm;
            return Json(await CategoryService.CategoryCreate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> CategoryUpdate([FromForm] CategoryCreateUpdateRequestModel Request)
        {
            return Json(await CategoryService.CategoryUpdate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> CategoryChangeState([FromBody] CategoryChangeStateRequestModel Request)
        {
            return Json(await CategoryService.CategoryChangeState(Request));
        }

        [HttpGet]
        public async Task<IActionResult> CategoryGet([FromRoute(Name = "id")] int CategoryID)
        {
            return Json(await CategoryService.CategoryGet(CategoryID));
        }

        [HttpPost]
        public async Task<IActionResult> CategoryDataTable(CategoryPaginationRequestModel DataTable)
        {
            DataTable.PageNumber = (DataTable.iDisplayStart / DataTable.iDisplayLength) + 1;
            DataTable.PageSize = DataTable.iDisplayLength;
            var ApiResponse = await CategoryService.CategoryPagination(DataTable);
            var Lista = ApiResponse.Data;
            var result = from sql in ApiResponse.Data.Items
                         select new[]{
                                 sql.CategoryID.ToString(),
                                 sql.CategoryName,
                                 sql.CategorySlug,                              
                                 SpanStateType((short)sql.RecordStateID),
                                 sql.CategoryLastUpdatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                                 sql.CategoryLastUpdatedUserName,
                                 sql.RecordStateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUpdate}):"&nbsp;",
                                 sql.RecordStateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUnchange}):LinkHRef(new ControlModel{Value=PermissionModel.AccChange})

             };
            return Json(new { sEcho = Convert.ToInt32(DataTable.sEcho), iTotalRecords = ApiResponse.Data.TotalRecords, iTotalDisplayRecords = ApiResponse.Data.RecordsFiltered, aaData = result });
        }
    }
}
