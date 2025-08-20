namespace SIGC.Infrastructure.CrossCutting.Constants
{
    public static class MessageDescriptionConst
    {
        //OPERACIONES DE REGISTROS
        public const string PROCESS_FULLYCOMPLETED = "ProcessSuccessfullyCompleted";        
        public const string PROCESS_INCOMPLETED = "ProcessIncompleted";
        public const string SATISFACTORY_INSERT = "SatisfactoryInsert";
        public const string SATISFACTORY_UPDATE = "SatisfactoryUpdate";
        public const string SATISFACTORY_DELETE = "SatisfactoryDelete";
        public const string SATISFACTORY_CHANGE = "SatisfactoryChange";
        public const string SATISFACTORY_INSERT_PROCESS = "InsertProcessSatisfactory";      
        //VALIDACIONES DE REGISTROS 
        public const string EXIST_INSERT = "ExistInsert";
        public const string EXIST_UPDATE = "ExistUpdate";
        public const string EXIST_INSERT_UPDATE = "ExistInsertUpdate";
        public const string EXIST_APPLICATION_CODE = "ExistApplicationCode";
        public const string EXIST_APPLICATION_NAME = "ExistApplicationName";
        public const string EXIST_COMPANY_DOCUMENTNUMBER = "ExistCompanyDocumentNumber";
        //ERRORES DE DE REGISTROS
        public const string ERROR_OBJECT = "ErrorObject";
        public const string ERROR_OPERATION = "ErrorOperation";
        public const string ERROR_INSERT = "ErrorInsert";
        public const string ERROR_UPDATE = "ErrorUpdate";
        public const string ERROR_CHANGE = "ErrorChange";
        public const string ERROR_DELETE = "ErrorDelete";
        public const string ERROR_EXCEPTION = "ErrorException";
        //QUERY RESULTADO
        public const string QUERY_RESULT = "QueryResult";
        public const string QUERY_EMPTY = "QueryEmpty";

        public const string INVALID_CREDENTIAL= "InvalidCredentials";
        public const string VALID_CREDENTIAL = "ValidCredentials";
        public const string INVALID_CREDENTIAL_DESCRIPTION = "El usuario y/o contraseña es incorrecta.";
        public const string VALID_CREDENTIAL_DESCRIPTION = "Credenciales verificadas,bienvenido al sistema.";
    }
}