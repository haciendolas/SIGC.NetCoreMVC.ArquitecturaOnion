namespace SIGC.Presentation.AspNetCoreMVC.Models
{
    public class LoginResponseModel
    {
        public int UserID { get; set; }  
        public string UserName { get; set; } = null!;
        public string UserFullName { get; set; } = null!;
        public string UserImage { get; set; } = null!;
        public string UserDocumentNumber { get; set; } = null!;
        public string CompanyDocumentNumber { get; set; } = null!;
    }
}
