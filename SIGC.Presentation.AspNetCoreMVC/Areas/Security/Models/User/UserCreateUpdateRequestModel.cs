namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.User
{
    public class UserCreateUpdateRequestModel{
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public string UserFirstName { get; set; } = null!;
        public string UserLastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string UserMail { get; set; } = null!;
        public short StateID { get; set; }
        public IFormFile? FormFile { get; set; }
        public string? UserPhoto { get; set; }
        public string? UserPhotoBandera { get; set; }
        public List<int> RoleIDs { get; set; } = new List<int>();
    }
}