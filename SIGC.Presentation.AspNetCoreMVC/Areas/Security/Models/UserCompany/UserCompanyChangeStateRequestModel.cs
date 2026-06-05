namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.UserCompany
{
    public class UserCompanyChangeStateRequestModel
    {
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public short StateID { get; set; }
    }
}