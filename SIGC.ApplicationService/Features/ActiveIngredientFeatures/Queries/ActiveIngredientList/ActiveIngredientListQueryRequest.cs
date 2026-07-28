using MediatR;
using SIGC.DomainModel.Dtos.ActiveIngredient;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.ActiveIngredientFeatures.Queries.ActiveIngredientList
{
    public sealed record ActiveIngredientListQueryRequest(        
    ) :IRequest<MsgResponse<List<ActiveIngredientListResponseDto>>>;
}