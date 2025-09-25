namespace SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleCreate
{
    public record struct RolePermissionCreateCommandRequest
    (
        int CompanyID,  
        int PageID,
        int PageActionID
     );
}
