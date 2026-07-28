using MediatR;
using SIGC.DomainModel.Dtos.Presentation;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PresentationFeatures.Queries.PresentationList
{
    public sealed record PresentationListQueryRequest(int UnitMeasureID
    ) :IRequest<MsgResponse<List<PresentationListResponseDto>>>;
}