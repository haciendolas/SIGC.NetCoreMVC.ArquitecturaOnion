using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryChangeState
{
    public record struct CategoryChangeStateCommandRequest
    (
      int CategoryId,
      RecordStateEnum RecordStateId
    ) :IRequest<MsgResponse<object?>>;    
}