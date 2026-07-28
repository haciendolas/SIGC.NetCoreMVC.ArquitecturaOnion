using MediatR;
using SIGC.DomainModel.Dtos.TherapeuticAction;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.TherapeuticActionFeatures.Queries.TherapeuticActionList
{
    public sealed record TherapeuticActionListQueryRequest(        
    ) :IRequest<MsgResponse<List<TherapeuticActionListResponseDto>>>;
}