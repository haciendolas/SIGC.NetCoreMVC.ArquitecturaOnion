namespace SIGC.Presentation.AspNetCoreMVC.Models.Auth
{
    public class AuthLoginTokenRequestModel
    {
        public string CompanyDocumentNumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
    }
}
