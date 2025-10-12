using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyChangeState
{
    public record struct CompanyChangeStateCommandRequest
    (
        int CompanyID,        
        StateEnum StateID
    ):IRequest<MsgResponse<object?>>;    
}
