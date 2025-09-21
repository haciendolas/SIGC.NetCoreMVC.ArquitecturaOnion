using System.ComponentModel;
using System.Reflection;

namespace SIGC.Presentation.AspNetCoreMVC.Helpers
{
    public class EnumsHelper
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
        
        public enum MessageDescription
        {
            //OPERACIONES DE REGISTROS
            [Description("El registro ha sido insertado satisfactoriamente")]
            InsertSatisfactory,
            [Description("Actualización del registro satisfactorio")]
            UpdateSatisfactory,
            [Description("El registro seleccionado ha sido eliminado correctamente")]
            DeleteSatisfactory,
            [Description("El registro seleccionado ha sido cambio de estado satisfactoriamente")]
            ChangeSatisfactory,
            [Description("La información se ha procesado satisfactoriamente")]
            InsertProcessSatisfactory,
            [Description("La devolución se ha procesado satisfactoriamente")]
            InsertProcessReturnSatisfactory,
            //Validacion de registros 
            [Description("El dato que quiere insertar ya existe")]
            ExistInsert,
            [Description("El dato que quiere modificar ya existe")]
            ExistUpdate,
            [Description("El dato que quiere insertar o modificar ya existe")]
            ExistInsertUpdate,
            [Description("El nombre del usuario existe")]
            ExistNombreUsuario,
            [Description("El D.N.I de la persona ya ha sido registrado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")]
            ExistDniPersona,
            [Description("El R.U.C de la persona ya ha sido registrado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")]
            ExistRucPersona,
            [Description("El nombre del usuario que desea insertar ya  existe")]
            ExistInsertNombreUsuario,
            [Description("El nombre del usuario que desea modificar ya  existe")]
            ExistUpdateNombreUsuario,
            [Description("El nombre del correo ya existe.&nbsp;&nbsp;")]
            ExistNombreEmail,
            [Description("El registro no puede ser eliminado por estar relacionado con otro registro")]
            ExistRelacionRegistro,
            [Description("El numero de ducumento ya existe.&nbsp;&nbsp;")]
            ExistNumeroDocumento,
            [Description("La información fue procesada y enviada al correo;")]
            SendMail,
            [Description("El Levantamiento de inspección fue enviada correctamente;")]
            SendLevantamientoInspeccion,
            [Description("La RIAC fue aprobada y enviada correctamente;")]
            SendFinalizarRiac,
            [Description("El Levantamiento de RIAC fue enviada correctamente;")]
            SendLevantamientoRiac,
            [Description("La anulación de la RIAC se realizó con exito;")]
            SendAnularRiac,
            //QUESTIONS
            [Description("¿Está seguro que desea cambiar el estado a este registro?")]
            QuestionChange,
            [Description("¿Esta seguro de eliminar este registro?")]
            QuestionDelete,
            [Description("¿Esta seguro de insertar este registro?")]
            QuestionInsert,
            [Description("¿Esta seguro de modificar este registro?")]
            QuestionUpdate,

            //Error de Operaciones
            [Description("Ha ocurrido un error con su petición, Comuniquese con su Proveedor de servicio.")]
            ErrorAjax,
            [Description("Objeto no ha sido encontrado.")]
            ErrorObject,
            [Description("No se pudo realizar la operación")]
            ErrorOperation,
            [Description("No se pudo insertar el registro")]
            ErrorInsert,
            [Description("No se pudo modificar el registro")]
            ErrorUpdate,
            [Description("No se pudo cambiar el estado del registro")]
            ErrorChange,
            [Description("El registro no se puede eliminar")]
            ErrorDelete,
            [Description("Se produjo un error de Exception")]
            ErrorException,

            //PROCESAMIENTO            
            [Description("Procesando información")]
            ProcessInformation,
            [Description("Cargando información")]
            LoadingInformation,
            [Description("Información no encontrada")]
            NotFoundInformation,

            //Verificaciones 
            [Description("Su Session se ha terminado...Vuelva a iniciar sessíon")]
            VerifyExpiredSession,
            [Description("Su Conexión a internet se ha perdido...Verifique su conexión a internet")]
            VerifyConnectionWWW,
            [Description("Verifique la configuracion del servidor de correo")]
            VerifyConfigServidorCorreo,
            [Description("No se encontro informacion ha procesar")]
            VerifyDataProcesar,
            [Description("El Perido no se encuentra aperturado")]
            VerifyPeriodo,

            //Requerimientos
            [Description("Dato requerido")]
            RequiredInformation,
            [Description("Debe agregar al menos una linea al detalle")]
            RequiredDetalleLinea,
            [Description("Debe elegir almenos una opcion del detalle de la lista")]
            RequiredSeleccionarDetalleLinea,
            [Description("Ingrese una fecha efectiva mayor o igual a la ultima fecha")]
            RequiredFecEfectivaMayor,

            //Query
            [Description("Consulta exitosa")]
            QueryResult,
            [Description("No se encontrarón registros")]
            QueryEmpty                           
        }
        
        public enum MessageTitle
        {
            [Description("Asistente de operaciones de registro")]
            AssistantOperation,

            [Description("Asistente de verificación de registro")]
            AssistantVerify,

            [Description("Asistente de error de registro")]
            AssistantError,

            [Description("Asistente de busqueda de información")]
            AssistantSearch,

            [Description("Asistente de detalle de lineas")]
            AssistantDetalleLinea,

            [Description("Asistente de verificación de sessión de usuario")]
            AssistantSession,

            [Description("Asistente de verificación de conexión a internet")]
            AssistantConnectionWWW
        } 

        public enum StateType
        {
            Inactive= 0,
            Active= 1,
            Delete = 2
        }
    }
}
