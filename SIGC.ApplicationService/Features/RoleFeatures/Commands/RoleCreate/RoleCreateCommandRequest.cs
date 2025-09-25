using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleCreate
{
    public record struct RoleCreateCommandRequest
    (
      int CompanyID,
      string RoleCode,
      string RoleName,
      string RoleDescription,
      StateEnum StateID,
      List<RolePermissionCreateCommandRequest> RolePermission
    ) :IRequest<MsgResponse<object?>>;    
}
