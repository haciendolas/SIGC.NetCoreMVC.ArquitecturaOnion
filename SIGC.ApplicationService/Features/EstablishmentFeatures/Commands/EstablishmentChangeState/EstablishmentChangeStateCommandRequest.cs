using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentChangeState
{
    public record struct EstablishmentChangeStateCommandRequest
    (
      int EstablishmentID,
      RecordStateEnum RecordStateID
    ) :IRequest<MsgResponse<object?>>;    
}