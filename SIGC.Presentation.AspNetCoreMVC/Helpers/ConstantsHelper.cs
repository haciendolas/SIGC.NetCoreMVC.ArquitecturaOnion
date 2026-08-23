namespace SIGC.Presentation.AspNetCoreMVC.Helpers
{
    public static class ConstantsHelper
    {
        public static class CustomClaimTypes
        {
            public const string USER_ID = "userID";
            public const string USER_NAME = "userName";
            public const string COMPANY_ID = "companyID";
            public const string IDIOM_ID = "idiomID";
            public const string COMPANY_DOCUMENTNUMBER = "companyDocumentNumber";
            public const string COMPANY_TRADENAME = "companyTradeName";
            public const string COMPANY_SOCIALREASON = "companySocialReason";
            public const string ROLE_CODES = "roleCodes";
        }

        public static class HttpClientNames
        {
            public const string ApiCommerce360 = "ApiCommerce360";
            public const string ApiAuth360 = "ApiAuth360";
        }

        public static class SessionKeys
        {
            public const string AuthenticationIdentity = "authenticationIdentity";
            public const string AccessToken = "accessToken";
            public const string RefreshToken = "refreshToken";
            public const string MenuSidebar = "menuSidebar";
            public const string Establishment = "establishment";
        }

        public static class UbigeoKeys
        {
            public const Int32 UbigeoClassContinente = 0;
            public const Int32 UbigeoClassAfrica = 91;
            public const Int32 UbigeoClassAmerica = 92;
            public const Int32 UbigeoClassAsia = 93;
            public const Int32 UbigeoClassEuropa = 94;
            public const Int32 UbigeoClassOceania = 95;
            public const String UbigeoCodePeru = "9200";
            public const Int32 UbigeoIDPeru = 38;
        }
        public static class TableKeys
        {
            public static class CurrencyType
            {
                public const int All = 1040;
                public const short SoleId = 1;
                public const short DolarId = 2;
                public const string DolarName = "Dolares";
                public const string DolarAbbreviation = "USD";
                public const short EuroId = 2;
            }

            public static class TaxAffectationType
            {
                public const int All = 1065;
                public const short GravadoName = 1;          
            }
        }
   }
}
