namespace SIGC.Infrastructure.CrossCutting.Constants
{
    public static class VerifyRegistryConst
    { 
        public static class Application
        {
            public const string OK = "OK";
            public const string CODE_EXISTS = "CODE_EXISTS";
            public const string NAME_EXISTS = "NAME_EXISTS";
        }

        public static class Category
        {
            public const string OK = "OK";            
            public const string NAME_EXISTS = "NAME_EXISTS";
        }

        public static class Company
        {
            public const string OK = "OK";
            public const string DOCUMENT_NUMBER_EXISTS = "DOCUMENT_NUMBER_EXISTS";      
        }

        public static class Role
        {
            public const string OK = "OK";
            public const string CODE_EXISTS = "CODE_EXISTS";
            public const string NAME_EXISTS = "NAME_EXISTS";
        }

        public static class User
        {
            public const string OK = "OK";
            public const string USER_EXISTS = "USER_EXISTS";
            public const string MAIL_EXISTS = "MAIL_EXISTS";
            public const string USER_AND_MAIL_EXISTS = "USER_AND_MAIL_EXISTS";
        }

        public static class Establishment
        {
            public const string OK = "OK";
            public const string NAME_EXISTS = "NAME_EXISTS";
        }

        public static class Warehouse
        {
            public const string OK = "OK";
            public const string NAME_EXISTS = "NAME_EXISTS";
        }
    }
}