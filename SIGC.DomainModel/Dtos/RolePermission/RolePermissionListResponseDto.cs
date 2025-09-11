namespace SIGC.DomainModel.Dtos.RolePermission
{
    public record struct RolePermissionListResponseDto
    (
        int PageID,
        int PageParentID,
        string PageHierarchy,
        string PageName,
        string PageUrlName,
        string PageIconName,
        short PageOrder
    );    
}