using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Company;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.CompanyService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Controllers
{
    [Area("Security")]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService CompanyService;
        public CompanyController(ICompanyService CompanyService)
        {
            this.CompanyService = CompanyService;
        }

        public IActionResult Index()
        {
            return View("CompanyIndex");
        }

        [HttpPost]
        public async Task<IActionResult> CompanyCreate([FromBody] CompanyCreateUpdateRequestModel Request)
        { 
            return Json(await CompanyService.CompanyCreate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> CompanyUpdate([FromBody] CompanyCreateUpdateRequestModel Request)
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
                PageSize = DataTable.iDisplayLength

            });
            var Lista = ApiResponse.Data;
            var result = from sql in ApiResponse.Data.Items
                         select new[]{
                                 sql.CompanyID.ToString(),
                                 sql.TaxpayerTypeName,
                                 sql.CompanyDocumentNumber,
                                 sql.CompanySocialReason,
                                 sql.SectorName,
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
