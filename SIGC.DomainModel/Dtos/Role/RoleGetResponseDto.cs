using SIGC.DomainModel.Dtos.RolePermission;

namespace SIGC.DomainModel.Dtos.Role
{
    public record struct RoleGetResponseDto
    (
        int RoleID,
        string RoleCode,
        string RoleName,
        string RoleDescription,
        short StateID,
        List<RolePermissionGetResponseDto> RolePermission
    );
}