using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Company;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Ubigeo;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.CompanyService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.ConstantService;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.UbigeoService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;
using System.Threading.Tasks;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Controllers
{
    [Area("Security")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService CompanyService;
        private readonly IConstantService ConstantService;
        private readonly IUbigeoService UbigeoService;
        public CompanyController(ICompanyService CompanyService, IConstantService ConstantService, IUbigeoService UbigeoService)
        {
            this.CompanyService = CompanyService;
            this.ConstantService = ConstantService;
            this.UbigeoService = UbigeoService;
        }

        public async Task<IActionResult> Index()
        {
            var ApiResponse = await ConstantService.ConstantList("1030,1034");
            ViewBag.TaxpayerTypeList = ApiResponse.Data.Where(w => w.ConstantClass == 1030 && w.ConstantID!=0).ToList();
            ViewBag.RubroList = ApiResponse.Data.Where(w => w.ConstantClass == 1034 && w.ConstantID != 0).ToList();

            ViewBag.CountryList = (await UbigeoService.UbigeoListByClassAndCodeAndLenCode(new UbigeoListByClassAndCodeAndLenCodeRequestModel
                                {
                                    UbigeoClass = ConstantsHelper.UbigeoKeys.UbigeoClassAmerica,
                                    UbigeoCode = ConstantsHelper.UbigeoKeys.UbigeoClassAmerica.ToString(),
                                    LenUbigeoCode = 4
                                })).Data;
            return View("CompanyIndex");
        }

        [HttpPost]
        public async Task<IActionResult> CompanyCreate([FromForm] CompanyCreateUpdateRequestModel Request)
        { 
            return Json(await CompanyService.CompanyCreate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> CompanyUpdate([FromForm] CompanyCreateUpdateRequestModel Request)
        {  
            return Json(await CompanyService.CompanyUpdate(Request));
        }

        [HttpPost]
        public async Task<IActionResult> CompanyChangeState([FromBody] CompanyChangeStateRequestModel Request)
        {            
            return Json(await CompanyService.CompanyChangeState(Request));
        }

        [HttpGet]
        public async Task<IActionResult> CompanyGet([FromRoute(Name = "id")] int CompanyID)
        {
            return Json(await CompanyService.CompanyGet(CompanyID));
        }

        [HttpPost(Name = "CompanyDataTable")]
        public async Task<IActionResult> CompanyDataTable(DataTableHelper DataTable)
        {
            var ApiResponse = await CompanyService.CompanyPagination(new CompanyPaginationRequestModel
            {
                CompanyIDRegister = GetSession().CompanyID,
                StateID = DataTable.sStateID,
                PageNumber = (DataTable.iDisplayStart / DataTable.iDisplayLength) + 1,
                PageSize = DataTable.iDisplayLength,
                TaxpayerTypeID = DataTable.sTaxpayerTypeID,
                RubroID =DataTable.sRubroID,
                CompanyDocumentNumber = DataTable.sCompanyDocumentNumber,
                CompanySocialReason = DataTable.sCompanySocialReason
            });
            var Lista = ApiResponse.Data;
            var result = from sql in ApiResponse.Data.Items
                         select new[]{
                                 sql.CompanyID.ToString(),
                                 sql.TaxpayerTypeName,
                                 sql.CompanyDocumentNumber,
                                 sql.CompanySocialReason,
                                 sql.RubroName,
                                 sql.CountryName,
                                 SpanStateType(sql.StateID),
                                 sql.CompanyLastUpdatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                                 sql.CompanyLastUpdatedUserName,
                                 sql.StateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUpdate}):"&nbsp:",
                                 sql.StateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUnchange}):LinkHRef(new ControlModel{Value=PermissionModel.AccChange})                             

             };
            return Json(new { sEcho = Convert.ToInt32(DataTable.sEcho), iTotalRecords = ApiResponse.Data.TotalRecords, iTotalDisplayRecords = ApiResponse.Data.RecordsFiltered, aaData = result });
        }
    }
}
