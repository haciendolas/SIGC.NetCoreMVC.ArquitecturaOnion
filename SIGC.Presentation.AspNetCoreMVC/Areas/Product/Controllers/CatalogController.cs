using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Catalog;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.BrandService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTypeService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CategoryService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.ManufacturerService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using System.Threading.Tasks;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Controllers
{
    [Area("Product")]
    public class CatalogController : BaseController
    {
        private readonly ICategoryService CategoryService;
        private readonly IBrandService BrandService;
        private readonly IManufacturerService ManufacturerService;
        private readonly ICatalogTypeService CatalogTypeService;
        private readonly ICatalogService CatalogService;

        public CatalogController(ICategoryService CategoryService,
            IBrandService BrandService,
            IManufacturerService ManufacturerService,
            ICatalogTypeService CatalogTypeService,
            ICatalogService CatalogService
        )
        {
            this.CategoryService = CategoryService;
            this.BrandService = BrandService;
            this.ManufacturerService = ManufacturerService;
            this.CatalogTypeService = CatalogTypeService;
            this.CatalogService = CatalogService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CatalogTypeList = (await CatalogTypeService.CatalogTypeList()).Data;
            ViewBag.CategoryList = (await CategoryService.CategoryList()).Data;
            ViewBag.ManufacturerList = (await ManufacturerService.ManufacturerList()).Data;
            ViewBag.BrandList = (await BrandService.BrandList()).Data;
            return View("CatalogIndex");
        }

        [HttpPost]
        public async Task<IActionResult> CatalogDataTable(CatalogPaginationRequestModel DataTable)
        {
            DataTable.PageNumber = (DataTable.iDisplayStart / DataTable.iDisplayLength) + 1;
            DataTable.PageSize = DataTable.iDisplayLength;
            var ApiResponse = await CatalogService.CatalogPagination(DataTable);
            var Lista = ApiResponse.Data;
            var result = from sql in ApiResponse.Data.Items
                         select new[]{
                                 sql.CatalogID.ToString(),
                                 sql.CatalogTypeName,
                                 sql.CatalogName,
                                 sql.CatalogVariantName,
                                 sql.CategoryName,
                                 sql.ManufacturerName,
                                 sql.BrandName,
                                 SpanStateType((short)sql.RecordStateID),
                                 sql.CatalogLastUpdatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                                 sql.CatalogLastUpdatedUserName,
                                 sql.RecordStateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUpdate}):"&nbsp;",
                                 sql.RecordStateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUnchange}):LinkHRef(new ControlModel{Value=PermissionModel.AccChange})

             };
            return Json(new { sEcho = Convert.ToInt32(DataTable.sEcho), iTotalRecords = ApiResponse.Data.TotalRecords, iTotalDisplayRecords = ApiResponse.Data.RecordsFiltered, aaData = result });
        }
    }
}
