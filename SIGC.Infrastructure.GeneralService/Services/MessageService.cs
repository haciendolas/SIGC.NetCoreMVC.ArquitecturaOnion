using Cross.Library.Service.Enums;
using Microsoft.Extensions.DependencyInjection;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;

namespace SIGC.Infrastructure.GeneralService.Services;

public class MessageService(IServiceProvider ServiceProvider) : IMessageService
{   
    private readonly ICurrentSessionService CurrentSessionAppService = ServiceProvider.GetRequiredService<ICurrentSessionService>();
    private readonly Dictionary<int, Dictionary<string, string>> _messages = GetMessages();

    public string GetMessageResult(string messageResultCode)
    {
        if (_messages.ContainsKey(CurrentSessionAppService.IsAuthenticated ? CurrentSessionAppService.IdiomID:(short)IdiomEnum.Spanish) &&
            _messages[CurrentSessionAppService.IsAuthenticated ? CurrentSessionAppService.IdiomID: (short)IdiomEnum.Spanish].ContainsKey(messageResultCode))
            return _messages[CurrentSessionAppService.IsAuthenticated ? CurrentSessionAppService.IdiomID:(short)IdiomEnum.Spanish][messageResultCode];

        return "¡Traductor de idioma no disponible!";
    }

    private static Dictionary<int, Dictionary<string, string>> GetMessages()
    {
        return new Dictionary<int, Dictionary<string, string>>
        {
            // Agregar los mensajes preestablecidos al diccionario
            [(short)IdiomEnum.English] = new()
            {
                { MessageDescriptionConst.PROCESS_FULLYCOMPLETED, "Process successfully completed." },
                { MessageDescriptionConst.PROCESS_INCOMPLETED, "The process could not be completed. Please try again." },
                { MessageDescriptionConst.SATISFACTORY_INSERT, "The record has been inserted successfully." },
                { MessageDescriptionConst.SATISFACTORY_UPDATE, "The registry was updated successfully." },
                { MessageDescriptionConst.SATISFACTORY_DELETE, "The selected record has been successfully deleted." },
                { MessageDescriptionConst.SATISFACTORY_CHANGE, "The selected record has been successfully changed state." },
                { MessageDescriptionConst.EXIST_INSERT, "The data you want to insert already exists." },
                { MessageDescriptionConst.EXIST_UPDATE, "The data you want to modify already exists." },
                { MessageDescriptionConst.EXIST_INSERT_UPDATE, "The data you want to insert or modify already exists." },
                { MessageDescriptionConst.ERROR_OBJECT, "Object has not been found." },
                { MessageDescriptionConst.ERROR_OPERATION, "The operation could not be performed." },
                { MessageDescriptionConst.ERROR_INSERT, "Could not insert record." },
                { MessageDescriptionConst.ERROR_UPDATE, "Could not modify the registry." },
                { MessageDescriptionConst.ERROR_CHANGE, "Could not change registry status." },
                { MessageDescriptionConst.ERROR_DELETE, "Record cannot be deleted." },
                { MessageDescriptionConst.ERROR_EXCEPTION, "An Exception error occurred." },
                { MessageDescriptionConst.EXIST_APPLICATION_CODE, "The application code already exists." },
                { MessageDescriptionConst.EXIST_APPLICATION_NAME, "The application name already exists." },
                { MessageDescriptionConst.QUERY_RESULT, "Successful query." },
                { MessageDescriptionConst.QUERY_EMPTY, "No results found." },
                { MessageDescriptionConst.VALID_CREDENTIAL, "Verified credentials, welcome to the system." },
                { MessageDescriptionConst.INVALID_CREDENTIAL, "The username and/or password is incorrect." },
                { MessageDescriptionConst.EXIST_COMPANY_DOCUMENTNUMBER, "The company document number already exists." },
                { MessageDescriptionConst.EXIST_COMPANY_SOCIALREASON, "The company social reason already exists." },
                { MessageDescriptionConst.EXIST_CATEGORY_CATEGORYNAME, "The category name already exists." },
                { MessageDescriptionConst.INVALID_JWT_TOKEN, "The token is invalid or has expired." },
                { MessageDescriptionConst.VALID_JWT_TOKEN, "The token is valid but not expired." },
                { MessageDescriptionConst.INVALID_RANDOM_TOKEN, "The refresh token is invalid or has expired." },
                { MessageDescriptionConst.EXIST_ROLE_ROLECODE, "The role code already exists." },
                { MessageDescriptionConst.EXIST_ROLE_ROLENAME, "The role name already exists." },
                { MessageDescriptionConst.EXIST_USER_USERNAME, "The user name already exists." },
                { MessageDescriptionConst.EXIST_USER_USERMAIL, "The user's email address already exists." },
                { MessageDescriptionConst.EXIST_USER_NAME_AND_MAIL, "The user and email already exist." },
                { MessageDescriptionConst.EXIST_ESTABLISHMENT_ESTABLISHMENTNAME, "The establishment name already exists." },
                { MessageDescriptionConst.EXIST_WAREHOUSE_WAREHOUSENAME, "The warehouse name already exists." }
            },

            [(short)IdiomEnum.Spanish] = new()
            {
                { MessageDescriptionConst.PROCESS_FULLYCOMPLETED, "Proceso completado correctamente." },
                { MessageDescriptionConst.PROCESS_INCOMPLETED, "El proceso no se pudo completar, intente nuevamente." },
                { MessageDescriptionConst.SATISFACTORY_INSERT, "El registro ha sido insertado satisfactoriamente." },
                { MessageDescriptionConst.SATISFACTORY_UPDATE, "El registro se actualizo satisfactorioamente." },
                { MessageDescriptionConst.SATISFACTORY_DELETE, "El registro seleccionado ha sido eliminado correctamente." },
                { MessageDescriptionConst.SATISFACTORY_CHANGE, "El registro seleccionado ha sido cambio de estado satisfactoriamente." },
                { MessageDescriptionConst.EXIST_INSERT, "El dato que quiere insertar ya existe." },
                { MessageDescriptionConst.EXIST_UPDATE, "El dato que quiere modificar ya existe." },
                { MessageDescriptionConst.EXIST_INSERT_UPDATE, "El dato que quiere insertar o modificar ya existe." },
                { MessageDescriptionConst.ERROR_OBJECT, "Objeto no ha sido encontrado." },
                { MessageDescriptionConst.ERROR_OPERATION, "No se pudo realizar la operación." },
                { MessageDescriptionConst.ERROR_INSERT, "No se pudo insertar el registro." },
                { MessageDescriptionConst.ERROR_UPDATE, "No se pudo modificar el registro." },
                { MessageDescriptionConst.ERROR_CHANGE, "No se pudo cambiar el estado del registro." },
                { MessageDescriptionConst.ERROR_DELETE, "El registro no se puede eliminar." },
                { MessageDescriptionConst.ERROR_EXCEPTION, "Se produjo un error de Exception." },
                { MessageDescriptionConst.EXIST_APPLICATION_CODE, "El código de la aplicación ya existe." },
                { MessageDescriptionConst.EXIST_APPLICATION_NAME, "El nombre de la aplicación ya existe." },
                { MessageDescriptionConst.QUERY_RESULT, "Consulta exitosa." },
                { MessageDescriptionConst.QUERY_EMPTY, "No se encontraron resultados." },
                { MessageDescriptionConst.VALID_CREDENTIAL, "Credenciales verificadas,bienvenido al sistema." },
                { MessageDescriptionConst.INVALID_CREDENTIAL, "El usuario y/o contraseña es incorrecta." },
                { MessageDescriptionConst.EXIST_COMPANY_DOCUMENTNUMBER, "El número del documento de la empresa ya existe." },
                { MessageDescriptionConst.EXIST_COMPANY_SOCIALREASON, "La razón social de la empresa ya existe." },
                { MessageDescriptionConst.EXIST_CATEGORY_CATEGORYNAME, "El nombre de la categoria ya existe." },
                { MessageDescriptionConst.INVALID_JWT_TOKEN, "El token es inválido o ha expirado." },
                { MessageDescriptionConst.VALID_JWT_TOKEN, "El token es válido aun no expirado." },
                { MessageDescriptionConst.INVALID_RANDOM_TOKEN, "El refresh token es inválido o ha expirado." },
                { MessageDescriptionConst.EXIST_ROLE_ROLECODE, "El código del rol ya existe." },
                { MessageDescriptionConst.EXIST_ROLE_ROLENAME, "El nombre de role ya existe." },
                { MessageDescriptionConst.EXIST_USER_USERNAME, "El nombre de usuario ya existe." },
                { MessageDescriptionConst.EXIST_USER_USERMAIL, "El correo del usuario ya existe." },
                { MessageDescriptionConst.EXIST_USER_NAME_AND_MAIL, "El usuario y el correo ya existe." },
                { MessageDescriptionConst.EXIST_ESTABLISHMENT_ESTABLISHMENTNAME, "El nombre del establecimiento ya existe." },
                { MessageDescriptionConst.EXIST_WAREHOUSE_WAREHOUSENAME, "El nombre del almacen ya existe." }
            }
        };
    }
}