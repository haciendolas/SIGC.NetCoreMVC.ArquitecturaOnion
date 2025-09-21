namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Helpers
{
    public static class ConstantsHelper
    {
        public static class Permissions
        {
            public static class User
            {
                public const string Create = "user:create";
                public const string Edit = "user:edit";
                public const string Delete = "user:delete";
            }

            public static class Role
            {
                public const string Create = "role:create";
            }
        }
    } 
}