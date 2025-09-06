namespace SIGC.Presentation.AspNetCoreMVC.Filters
{
    public class AuthenticationIdentity
    {
        public int UserID { get; set; }  
        public string UserName { get; set; } = null!;
        public string UserFirstName { get; set; } = null!;
        public string UserLastName { get; set; } = null!;
        public string UserFullName { get; set; } = null!;
        public string UserImage { get; set; } = null!;
        public string UserMail { get; set; } = null!;  
        public int CompanyID { get; set; }
        public short IdiomID { get; set; }
        public string CompanyDocumentNumber { get; set; } = null!;
        public string CompanyTradeName { get; set; } = null!;
        public string CompanySocialReason { get; set; } = null!;
        public List<string> RoleList { get; set; } = null!;
    }
}