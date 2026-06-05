using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Establishment;
using SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Services.EstablishmentService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class EstablishmentController : BaseController
    {
        private readonly IEstablishmentService EstablishmentService;
        public EstablishmentController(IEstablishmentService EstablishmentService)
        {
            this.EstablishmentService = EstablishmentService;
        }

        public IActionResult Index()
        {
            return View("EstablishmentIndex");
        }

        [HttpPost]
        public async Task<IActionResult> EstablishmentCreate([FromForm] EstablishmentCreateUpdateRequestModel Request)
        {
            Request.RecordOriginId = (byte)EnumsHelper.RecordOrigin.WebForm;
            return Json(await EstablishmentService.EstablishmentCreate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> EstablishmentUpdate([FromForm] EstablishmentCreateUpdateRequestModel Request)
        {            
            return Json(await EstablishmentService.EstablishmentUpdate(Request));
        }

        [HttpPut]
        public async Task<IActionResult> EstablishmentChangeState([FromBody] EstablishmentChangeStateRequestModel Request)
        {
            return Json(await EstablishmentService.EstablishmentChangeState(Request));
        }

        [HttpGet]
        public async Task<IActionResult> EstablishmentGet([FromRoute(Name = "id")] int EstablishmentID)
        {
            return Json(await EstablishmentService.EstablishmentGet(EstablishmentID));
        }

        [HttpPost]
        public async Task<IActionResult> EstablishmentDataTable(EstablishmentPaginationRequestModel DataTable)
        {
            DataTable.PageNumber = (DataTable.iDisplayStart / DataTable.iDisplayLength) + 1;
            DataTable.PageSize = DataTable.iDisplayLength;
            var ApiResponse = await EstablishmentService.EstablishmentPagination(DataTable);
            var Lista = ApiResponse.Data;
            var result = from sql in ApiResponse.Data.Items
                         select new[]{
                                 sql.EstablishmentID.ToString(),
                                 sql.EstablishmentCode,
                                 sql.EstablishmentName,
                                 sql.EstablishmentAddress,
                                 SpanStateType(sql.RecordStateID),
                                 sql.EstablishmentLastUpdatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                                 sql.EstablishmentLastUpdatedUserName,
                                 sql.RecordStateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUpdate}):"&nbsp;",
                                 sql.RecordStateID==(short)EnumsHelper.StateType.Active ? LinkHRef(new ControlModel{Value=PermissionModel.AccUnchange}):LinkHRef(new ControlModel{Value=PermissionModel.AccChange})

             };
            return Json(new { sEcho = Convert.ToInt32(DataTable.sEcho), iTotalRecords = ApiResponse.Data.TotalRecords, iTotalDisplayRecords = ApiResponse.Data.RecordsFiltered, aaData = result });
        }
    }
}
