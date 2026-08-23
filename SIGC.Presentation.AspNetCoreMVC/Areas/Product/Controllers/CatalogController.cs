using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Catalog;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.ActiveIngredientService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.AttributeService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.BrandService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CatalogTypeService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.CategoryService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.ManufacturerService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PharmaceuticalFormService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PrescriptionTypeService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.PriceTypeService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.TherapeuticActionService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Product.Services.UnitMeasureService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.ConstantService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;

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
        private readonly IActiveIngredientService ActiveIngredientService;
        private readonly IPharmaceuticalFormService PharmaceuticalFormService;
        private readonly IPrescriptionTypeService PrescriptionTypeService;
        private readonly ITherapeuticActionService TherapeuticActionService;
        private readonly IUnitMeasureService UnitMeasureService;
        private readonly IPriceTypeService PriceTypeService;
        private readonly IAttributeService AttributeService;
        private readonly IConstantService ConstantService;

        public CatalogController(ICategoryService CategoryService,
            IBrandService BrandService,
            IManufacturerService ManufacturerService,
            ICatalogTypeService CatalogTypeService,
            ICatalogService CatalogService,
            IActiveIngredientService ActiveIngredientService,
            IPharmaceuticalFormService PharmaceuticalFormService,
            IPrescriptionTypeService PrescriptionTypeService,
            ITherapeuticActionService TherapeuticActionService,
            IUnitMeasureService UnitMeasureService,
            IPriceTypeService PriceTypeService,
            IAttributeService AttributeService,
            IConstantService ConstantService
        )
        {
            this.CategoryService = CategoryService;
            this.BrandService = BrandService;
            this.ManufacturerService = ManufacturerService;
            this.CatalogTypeService = CatalogTypeService;
            this.CatalogService = CatalogService;
            this.ActiveIngredientService = ActiveIngredientService;
            this.PharmaceuticalFormService = PharmaceuticalFormService;
            this.PrescriptionTypeService = PrescriptionTypeService;
            this.TherapeuticActionService = TherapeuticActionService;
            this.UnitMeasureService = UnitMeasureService;
            this.PriceTypeService = PriceTypeService;
            this.AttributeService = AttributeService;
            this.ConstantService = ConstantService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CatalogTypeList = (await CatalogTypeService.CatalogTypeList()).Data;
            ViewBag.CategoryList = (await CategoryService.CategoryList()).Data;
            ViewBag.ManufacturerList = (await ManufacturerService.ManufacturerList()).Data;
            ViewBag.BrandList = (await BrandService.BrandList()).Data;
            ViewBag.ActiveIngredientList = (await ActiveIngredientService.ActiveIngredientList()).Data;
            ViewBag.PharmaceuticalFormList = (await PharmaceuticalFormService.PharmaceuticalFormList()).Data;
            ViewBag.PrescriptionTypeList = (await PrescriptionTypeService.PrescriptionTypeList()).Data;
            ViewBag.TherapeuticActionList = (await TherapeuticActionService.TherapeuticActionList()).Data;
            ViewBag.UnitMeasureList = (await UnitMeasureService.UnitMeasureList()).Data;
            ViewBag.PriceTypeList = (await PriceTypeService.PriceTypeList()).Data; 
            ViewBag.AttributeList = (await AttributeService.AttributeValueList(true)).Data;
            var ConstantList = (await ConstantService.ConstantList($"{ConstantsHelper.TableKeys.CurrencyType.All},{ConstantsHelper.TableKeys.TaxAffectationType.All}")).Data!;
            ViewBag.CurrencyTypeList = ConstantList.Where(w => w.ConstantClass == ConstantsHelper.TableKeys.CurrencyType.All && w.ConstantID != 0).ToList();
            ViewBag.TaxAffectationTypeList = ConstantList.Where(w => w.ConstantClass == ConstantsHelper.TableKeys.TaxAffectationType.All && w.ConstantID != 0).ToList();
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
