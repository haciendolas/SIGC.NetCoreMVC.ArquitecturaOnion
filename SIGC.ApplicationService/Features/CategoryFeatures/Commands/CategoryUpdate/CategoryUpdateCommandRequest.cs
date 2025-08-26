using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryUpdate
{
    public record struct CategoryUpdateCommandRequest
    (
        int CategoryId,
        string CategoryName,
        StateEnum StateId
    ):IRequest<MsgResponse<object?>>;    
}