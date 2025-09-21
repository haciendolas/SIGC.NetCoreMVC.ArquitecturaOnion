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
        }
    }
}
