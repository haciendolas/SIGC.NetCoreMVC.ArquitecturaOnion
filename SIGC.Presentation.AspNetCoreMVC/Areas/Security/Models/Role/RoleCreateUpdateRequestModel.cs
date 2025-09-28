namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Role
{
    public class RoleCreateUpdateRequestModel
    {
        public int RoleID { get; set; }
        public int CompanyID { get; set; }
        public string RoleCode { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public string RoleDescription { get; set; } = null!;
        public short StateID { get; set; }
        public List<RolePermissionCreateRequestModel> RolePermission { get; set; } = new List<RolePermissionCreateRequestModel>();
    }

    public class RolePermissionCreateRequestModel
    {
        public int CompanyID { get; set; }
        public int PageID { get; set; }
        public int PageActionID { get; set; }
    }
}