using MediatR;
using SIGC.DomainModel.Dtos.Establishment;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentList
{
    public sealed record EstablishmentListQueryRequest
    (
        int? PersonID
    ) :IRequest<MsgResponse<List<EstablishmentListResponseDto>>>;
}