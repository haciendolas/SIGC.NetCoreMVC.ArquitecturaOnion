namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role
{
    public class RolePaginationResponseModel
    {
        public int RoleID { get; set; }
        public string RoleCode { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string? RoleDescription { get; set; }
        public DateTime RoleLastUpdatedDateTime { get; set; }
        public string RoleLastUpdatedUserName { get; set; } = null!;
        public short StateID { get; set; }
    }
}
