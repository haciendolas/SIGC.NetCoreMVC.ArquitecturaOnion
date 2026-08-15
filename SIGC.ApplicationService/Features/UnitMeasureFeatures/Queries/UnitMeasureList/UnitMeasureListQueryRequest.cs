using MediatR;
using SIGC.DomainModel.Dtos.UnitMeasure;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UnitMeasureFeatures.Queries.UnitMeasureList
{
    public sealed record UnitMeasureListQueryRequest(        
    ) :IRequest<MsgResponse<List<UnitMeasureListResponseDto>>>;
}