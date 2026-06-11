using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Warehouse;
using SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Services.WarehouseService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class WarehouseController : BaseController
    {
        private readonly IWarehouseService WarehouseService;
        public WarehouseController(IWarehouseService WarehouseService)
        {
            this.WarehouseService = WarehouseService;
        }

        public IActionResult Index()
        {
            return View("WarehouseIndex");
        }

        [HttpPost]
        public async Task<IActionResult> WarehouseCreate([FromBody] WarehouseCreateUpdateRequestModel Request)
        {
            Request.RecordOriginID = (byte)EnumsHelper.RecordOrigin.WebForm;
            return Json(await WarehouseService.WarehouseCreate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> WarehouseUpdate([FromBody] WarehouseCreateUpdateRequestModel Request)
        {      
            return Json(await WarehouseService.WarehouseUpdate(Request));
        }

        [HttpGet]
        public async Task<IActionResult> WarehouseGet([FromRoute(Name = "id")] int WarehouseID)
        {
            return Json(await WarehouseService.WarehouseGet(WarehouseID));
        }

        [HttpPost]
        public async Task<IActionResult> WarehouseDataTable(WarehousePaginationRequestModel DataTable)
        {
            DataTable.PageNumber = (DataTable.iDisplayStart / DataTable.iDisplayLength) + 1;
            DataTable.PageSize = DataTable.iDisplayLength;
            var ApiResponse = await WarehouseService.WarehousePagination(DataTable);
            var Lista = ApiResponse.Data;
            var result = from sql in ApiResponse.Data.Items
                         select new[]{
                                 sql.EstablishmentCodeAndName,
                                 sql.WarehouseID.ToString(),
                                 sql.WarehouseCode,
                                 sql.WarehouseName,                          
                                 SpanStateType(sql.RecordStateID),
                                 sql.WarehouseLastUpdatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                                 sql.WarehouseLastUpdatedUserName,
                                 sql.RecordStateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUpdate}):"&nbsp;",
                                 sql.RecordStateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUnchange}):LinkHRef(new ControlModel{Value=PermissionModel.AccChange})

             };
            return Json(new { sEcho = Convert.ToInt32(DataTable.sEcho), iTotalRecords = ApiResponse.Data.TotalRecords, iTotalDisplayRecords = ApiResponse.Data.RecordsFiltered, aaData = result });
        }
    }
}
