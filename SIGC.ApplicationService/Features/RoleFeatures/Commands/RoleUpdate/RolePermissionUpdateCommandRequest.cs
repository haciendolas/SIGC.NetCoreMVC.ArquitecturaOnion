namespace SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleUpdate
{
    public record struct RolePermissionUpdateCommandRequest
    (
        int PageID,
        int PageActionID
    );
}