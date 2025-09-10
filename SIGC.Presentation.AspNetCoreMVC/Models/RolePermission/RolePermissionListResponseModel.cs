namespace SIGC.Presentation.AspNetCoreMVC.Models.RolePermission
{
    public class RolePermissionListResponseModel
    {
        public int PageID { get; set; }
        public int PageParentID { get; set; }
        public string PageHierarchy { get; set; } = null!;        
        public string PageName { get; set; } = null!;
        public string PageUrlName { get; set; } = null!;
        public string PageIconName { get; set; } = null!;
    }
}