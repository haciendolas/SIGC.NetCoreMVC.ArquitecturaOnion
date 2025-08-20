namespace SIGC.Presentation.AspNetCoreMVC.Models
{
    public class LoginRequestModel
    {
        public string CompanyDocumentNumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
    }
}
