using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryChangeState
{
    public record struct CategoryChangeStateCommandRequest
    (
      int CategoryId,
      StateEnum StateId
    ):IRequest<MsgResponse<object?>>;    
}