using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.RoleService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using System;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Controllers
{
    [Area("Security")]
    public class RoleController : BaseController
    {
        private readonly IRoleService RoleService;
        public RoleController(IRoleService RoleService)
        {
            this.RoleService = RoleService;
        }
        public IActionResult Index(){           
            return View("RoleIndex");
        }

        [HttpPost(Name = "RolePagination")]
        public async Task<IActionResult> RolePagination(DataTableHelper DataTable)
        {
            var ApiResponse = await RoleService.RolePagination(new RolePaginationRequestModel
            {
                CompanyID = 1,
                StateID = DataTable.sStateID,
                PageNumber = (DataTable.iDisplayStart / DataTable.iDisplayLength) + 1,
                PageSize = DataTable.iDisplayLength,
                Search = DataTable.sSearch ?? ""

            });
            // List<BE_VenSerie> Lista = BL_VenSerie.jcr_PaginationVenSerie_By_cPerJurCodigo_nVenSerieTipoComprobante_cVenSerieNumero_nPaginaInicia_nTotalPaginas(parametros.scPerJurCodigo, parametros.snCampo, parametros.scCampo, parametros.iDisplayStart, parametros.iDisplayLength);

            var Lista = ApiResponse.Data; 
      
            var result = from sql in ApiResponse.Data.Items
                         select new[]{
                                 
                                 sql.RoleID.ToString(),
                                 sql.RoleCode,
                                 sql.RoleName,
                                 sql.StateID==1 ? "<span class='label label-success rounded'>Activo</span>":"<span class='label label-danger rounded'>Inactivo</span>",
                                 sql.RoleLastUpdatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                                 sql.RoleLastUpdatedUserName,
                                 ""                                
             };
            return Json(new { sEcho = Convert.ToInt32(DataTable.sEcho), iTotalRecords = ApiResponse.Data.Count, iTotalDisplayRecords = ApiResponse.Data.Count, aaData = result });
        }
    }
}
