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
                    control = "<a name=linkPrint href=javascript:void(0) class=\"btn btn-info btn-xs rounded tooltips\" data-toggle=\"tooltip\" data-placement=\"top\" data-title=\"Vista Impresión\" " + (Control.Property ?? "") + "><i class=\"glyphicon glyphicon-print\"></i></a>";
                    break;
                case PermissionModel.AccExport:
                    control = "<a name=linkExport href=javascript:void(0) class=\"btn btn-info btn-xs rounded tooltips\" data-toggle=\"tooltip\" data-placement=\"top\" data-title=\"Download PDF\" " + (Control.Property ?? "") + "><i class=\"glyphicon glyphicon-download-alt\"></i></a>";
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
        
   }
}