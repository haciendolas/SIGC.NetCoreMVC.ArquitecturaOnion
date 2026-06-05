namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.User
{
    public class UserPaginationResponseModel
    {
        public int UserID { get; set; }
        public string UserFirstName { get; set; } = null!;
        public string UserLastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? UserMail { get; set; }
        public short StateID { get; set; }
        public string UserRolNames { get; set; } = null!;
        public DateTime UserLastUpdatedDateTime { get; set; }
        public string UserLastUpdatedUserName { get; set; } = null!;
    }
}