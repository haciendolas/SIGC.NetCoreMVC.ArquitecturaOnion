using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.CategoryCreate
{
    public record struct CategoryCreateCommandRequest
     (
        string CategoryCode,
        string CategoryName,
        StateEnum StateId
     ) : IRequest<MsgResponse<object>>;
}
