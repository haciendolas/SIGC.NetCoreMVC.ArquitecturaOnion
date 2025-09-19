using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role;
using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Services.RoleService;
using SIGC.Presentation.AspNetCoreMVC.Controllers;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                                 sql.StateID==1 ? "<span class='badge badge-soft-success text-uppercase fs-14'><i class='ri-checkbox-circle-line align-bottom'></i> Activo</span>":"<span class='badge badge-soft-danger text-uppercase fs-14'><i class='ri-close-circle-line align-bottom'></i> Inactivo</span>",
                                 sql.RoleLastUpdatedDateTime.ToString("dd/MM/yyyy hh:mm:ss"),
                             sql.RoleLastUpdatedUserName,
                             "<a href='javascript:void(0);' name=slnkEdit data-bs-toggle='tooltip' data-bs-placement='top' title='Editar' data-title='Editar' class='link-primary'><i class='ri-pencil-fill fs-24'></i></a>",
                             "<a href='javascript:void(0);' name=slnkInactive data-bs-toggle='tooltip' data-bs-placement='top' title='Desactivar' data-title='Desactivar' class='link-success'><i class='ri-delete-bin-line fs-24'></i></a>",
                             "<a href='javascript:void(0);' name=slnkActive data-bs-toggle='tooltip' data-bs-placement='top' title='Activar' data-title='Activar' class='link-success'><i class='ri-refresh-line fs-24'></i></a>",
                             "<a href='javascript:void(0);' name=slnkDelete data-bs-toggle='tooltip' data-bs-placement='top' title='Eliminar' data-title='Eliminar' class='link-danger'><i class='ri-close-line fs-1'></i></a>"

             };
            return Json(new { sEcho = Convert.ToInt32(DataTable.sEcho), iTotalRecords = ApiResponse.Data.TotalRecords, iTotalDisplayRecords = ApiResponse.Data.RecordsFiltered, aaData = result });
        }
    }
}
