using MediatR;
using SIGC.DomainModel.Dtos.RolePermission;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RolePermissionFeatures.Queries.RolePermissionList
{
    public record struct RolePermissionListQueryRequest
    (
     int UserID,
     int CompanyID
    ):IRequest<MsgResponse<List<RolePermissionListResponseDto>>>;  
}