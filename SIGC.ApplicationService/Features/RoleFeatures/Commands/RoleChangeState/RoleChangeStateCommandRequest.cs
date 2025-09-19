using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleChangeState
{
    public record struct RoleChangeStateCommandRequest
    (
        int CompanyID,
        int RoleID,
        StateEnum StateID
    ):IRequest<MsgResponse<object?>>;    
}
