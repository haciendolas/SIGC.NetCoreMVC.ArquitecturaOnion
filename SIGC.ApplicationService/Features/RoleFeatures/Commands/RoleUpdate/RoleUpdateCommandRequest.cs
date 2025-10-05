using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleUpdate
{
    public record struct RoleUpdateCommandRequest
    (
      int RoleID,
      int CompanyID,
      string RoleCode,
      string RoleName,
      string RoleDescription,
      StateEnum StateID,
      List<RolePermissionUpdateCommandRequest> RolePermission
    ): IRequest<MsgResponse<object?>>;
}