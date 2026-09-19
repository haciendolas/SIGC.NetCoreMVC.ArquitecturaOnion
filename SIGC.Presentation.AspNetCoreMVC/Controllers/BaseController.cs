using Microsoft.AspNetCore.Mvc;
using SIGC.Presentation.AspNetCoreMVC.Extensions;
using SIGC.Presentation.AspNetCoreMVC.Filters;
using SIGC.Presentation.AspNetCoreMVC.Helpers;
using SIGC.Presentation.AspNetCoreMVC.Models;

namespace SIGC.Presentation.AspNetCoreMVC.Controllers
{
    public class BaseController : Controller
    {
        public AuthenticationIdentity GetSession()
        {            
            return HttpContext.Session.GetObject<AuthenticationIdentity>(ConstantsHelper.SessionKeys.AuthenticationIdentity) ?? new AuthenticationIdentity();
        }

        public string LinkHRef(ControlModel Control)
        { 
            string control = string.Empty;
            switch (Control.Value)
            {
                case PermissionModel.AccUpdate:
                    control = "<a href=\"javascript:void(0)\" name=\"slnkEdit\" data-bs-toggle=\"tooltip\" data-bs-placement=\"top\" data-title=\"Editar\" title=\"Editar\" class=\"link-primary\" " +( Control.Property ?? "" )+ "><i class=\"ri-pencil-fill fs-24\"></i></a>";
                    break;
                case PermissionModel.AccDelete:
                    control = "<a href=\"javascript:void(0)\" name=\"slnkDelete\" data-bs-toggle=\"tooltip\" data-bs-placement=\"top\" data-title=\"Eliminar\" title=\"Eliminar\" class=\"link-danger\" " + (Control.Property ?? "" )+ "><i class=\"ri-close-line fs-24\"></i></a>";
                    break;
                case PermissionModel.AccChange:
                    control = "<a href=\"javascript:void(0)\" name=\"slnkActive\" data-bs-toggle=\"tooltip\" data-bs-placement=\"top\" data-title=\"Activar\" title=\"Activar\" class=\"link-success\" " + (Control.Property ?? "") + "><i class=\"ri-refresh-line fs-24\"></i></a>";
                    break;
                case PermissionModel.AccUnchange:
                    control = "<a href=\"javascript:void(0)\" name=\"slnkInactive\" data-bs-toggle=\"tooltip\" data-bs-placement=\"top\" data-title=\"Desactivar\" title=\"Desactivar\" class=\"link-success\" " + (Control.Property ?? "") + "><i class=\"ri-delete-bin-line fs-24\"></i></a>";
                    break;
                case PermissionModel.AccPrint:
                    control = "<a href=javascript:void(0) name=slnkPrint data-bs-toggle=\"tooltip\" data-bs-placement=\"top\" data-title=\"Vista Impresión\" title=\"Vista Impresión\" " + (Control.Property ?? "") + "><i class=\"ri-printer-line fs-24\"></i></a>";
                    break;
                case PermissionModel.AccExport:
                    control = "<a href=javascript:void(0) name=slnkExport data-bs-toggle=\"tooltip\" data-bs-placement=\"top\" data-title=\"Descargar\" " + (Control.Property ?? "") + "><i class=\"ri-download-2-line fs-24\"></i></a>";
                    break;
                default:
                    control = "";
                    break;
            }
            return control;      
        }

        public string SpanStateType(short StateID)
        {
            string MyHtml = "<span class='badge badge-soft-success text-uppercase fs-14'><i class='ri-checkbox-circle-line align-bottom'></i> Activo</span>";
            if(StateID == (short)EnumsHelper.StateType.Inactive)
            {
                MyHtml = "<span class='badge badge-soft-danger text-uppercase fs-14'><i class='ri-close-circle-line align-bottom'></i> Inactivo</span>";
            }
            return MyHtml;
        }

        public static string LinkUL(List<ControlModel> Controls, Boolean IsVisible)
        {
            var myBotons = "<div class='dropdown d-inline-block'>";
            myBotons += "<a class='btn btn-default btn-sm dropdown' data-bs-toggle=dropdown aria-expanded=false>";
            myBotons += "<i class='ri-more-2-fill align-middle fs-20'></i>";
            myBotons += "</a>";
            myBotons += "<ul class='dropdown-menu dropdown-menu-end'>";
            foreach (var item in Controls)
            {
                if (item.IsVisible.HasValue) IsVisible = item.IsVisible.Value;
                switch (item.Value)
                {
                    case PermissionModel.AccInsert:
                        if (IsVisible)
                        {
                            myBotons += "<li><a class='dropdown-item text-primary' name=lnkSave href=javascript:void(0) " + item.Property + "><i class=\"ri-add-fill align-middle me-2 fs-15\"></i>Agregar</a></li>";
                            if(item.ShowDivider)
                             myBotons += "<li class=dropdown-divider" + item.Style + "></li>";
                        }
                        break;
                    case PermissionModel.AccUpdate:
                        if (IsVisible)
                        {
                            myBotons += "<li " + item.Style + "><a class='dropdown-item text-primary' name=lnkEdit href=javascript:void(0) " + item.Property + "><i class=\"ri-pencil-fill align-middle me-2 fs-15\"></i>Editar</a></li>";
                            if (item.ShowDivider)
                                myBotons += "<li class=dropdown-divider " + item.Style + "></li>";
                        }
                        break;
                    case PermissionModel.AccDelete:
                        myBotons += "<li  " + item.Style + "><a class='dropdown-item text-danger' name=lnkDelete href=javascript:void(0) " + item.Property + "><i class=\"ri-close-line align-middle me-2 fs-15\"></i>Eliminar</a></li>";
                        if (item.ShowDivider)
                            myBotons += "<li class=dropdown-divider></li>";
                        break;
                    case PermissionModel.AccChange:
                        if (!IsVisible)
                        {
                            myBotons += "<li  " + item.Style + "><a class='dropdown-item text-success' name=lnkActive href=javascript:void(0) " + item.Property + "><i class=\"ri-refresh-line align-middle me-2 fs-15\"></i>" + (item.Label != null ? item.Label : "Activar") + "</a></li>";
                            if (item.ShowDivider)
                                myBotons += "<li class=dropdown-divider></li>";
                        }
                        break;
                    case PermissionModel.AccUnchange:
                        if (IsVisible)
                        {
                            myBotons += "<li  " + item.Style + "><a class='dropdown-item text-success' name=lnkInactive href=javascript:void(0) " + item.Property + "><i class=\"ri-delete-bin-line align-middle me-2 fs-15\"></i>" + (item.Label != null ? item.Label : "Desactivar") + "</a></li>";
                            if (item.ShowDivider)
                                myBotons += "<li class=dropdown-divider></li>";
                        }
                        break;
                    case PermissionModel.AccPrint:
                        myBotons += "<li " + item.Style + "><a class='dropdown-item text-info' name=" + (item.Name != null ? item.Name : "lnkPrint") + " href=javascript:void(0) " + item.Property + "><i class=\"ri-printer-line align-middle me-2 fs-15\"></i>" + (item.Label != null ? item.Label : "Imprimir") + "</a></li>";
                        if (item.ShowDivider)
                            myBotons += "<li class=dropdown-divider " + item.Style + "></li>";
                        break;
                    case PermissionModel.AccExport:
                        myBotons += "<li " + item.Style + "><a class='dropdown-item text-info' name=" + (item.Name != null ? item.Name : "lnkExport") + " href=javascript:void(0) " + item.Property + "><i class=\"ri-download-2-line align-middle me-2 fs-15\"></i>" + (item.Label != null ? item.Label : "Exportar") + "</a></li>";
                        if (item.ShowDivider)
                            myBotons += "<li class=dropdown-divider " + item.Style + "></li>";
                        break;
                    case PermissionModel.AccAdd:
                        if (IsVisible)
                        {
                            myBotons += "<li " + item.Style + "><a class='dropdown-item text-primary' name=" + (item.Name != null ? item.Name : "lnkAdd") + " href =javascript:void(0) " + item.Property + "><i class=\"" + (item.Icon != null ? item.Icon : "ri-add-fill") + " align-middle me-2 fs-15\"></i>" + (item.Label != null ? item.Label : "Agregar") + "</a></li>";
                            if (item.ShowDivider)
                                myBotons += "<li class=dropdown-divider " + item.Style + "></li>";
                        }
                        break;
                    case PermissionModel.AccApprove:
                        if (IsVisible)
                        {
                            myBotons += "<li " + item.Style + "><a class='dropdown-item text-success' name=" + (item.Name != null ? item.Name : "lnkApprove") + " href=javascript:void(0) " + item.Property + "><i class=\"" + (item.Icon != null ? item.Icon : "fa fa-hand-o-right") + "\"></i>&nbsp;" + (item.Label != null ? item.Label : "Aprobar") + "</a></li>";
                            if (item.ShowDivider)
                                myBotons += "<li class=dropdown-divider " + item.Style + "></li>";
                        }
                        break;
                    case PermissionModel.AccView:
                        if (IsVisible)
                        {
                            myBotons += "<li " + item.Style + "><a class='dropdown-item text-primary' name=" + (item.Name != null ? item.Name : "lnkView") + " href=javascript:void(0) " + item.Property + "><i class=\"" + (item.Icon != null ? item.Icon : "fa fa-eye") + "\"></i>&nbsp;" + (item.Label != null ? item.Label : "Ver detalle") + "</a></li>";
                            if (item.ShowDivider)
                                myBotons += "<li class=dropdown-divider " + item.Style + "></li>";
                        }
                        break;
                    case PermissionModel.AccFinish:
                        if (IsVisible)
                        {
                            myBotons += "<li " + item.Style + "><a class='dropdown-item text-success' name=" + (item.Name != null ? item.Name : "lnkFinish") + " href=javascript:void(0) " + item.Property + "><i class=\"" + (item.Icon != null ? item.Icon : "fa fa-hand-o-right") + "\"></i>&nbsp;" + (item.Label != null ? item.Label : "Finalizar") + "</a></li>";
                            if (item.ShowDivider)
                                myBotons += "<li class=dropdown-divider " + item.Style + "></li>";
                        }
                        break;
                    default:
                        myBotons += "";
                        break;
                }
            }
            myBotons += "</ul>";
            myBotons += "</div>";
            return myBotons;
        }

    }
}