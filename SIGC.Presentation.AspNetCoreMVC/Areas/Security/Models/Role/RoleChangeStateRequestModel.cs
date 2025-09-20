namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role
{
    public class RoleChangeStateRequestModel
    {
        public int CompanyID { get; set; }
        public int RoleID { get; set; }
        public short StateID { get; set; }
    }
}