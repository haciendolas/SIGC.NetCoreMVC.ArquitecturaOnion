namespace SIGC.DomainModel.ValueObjects
{
    public record struct RolePermission
    (
         int CompanyID,
         int RoleID,
         int PageID,
         int PageActionID,
         DateTime PageRoleCreatedDateTime
    );
}