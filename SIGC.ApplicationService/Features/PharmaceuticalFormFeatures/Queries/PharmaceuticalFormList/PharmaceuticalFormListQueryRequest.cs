using MediatR;
using SIGC.DomainModel.Dtos.PharmaceuticalForm;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PharmaceuticalFormFeatures.Queries.PharmaceuticalFormList
{
    public sealed record PharmaceuticalFormListQueryRequest(        
    ) :IRequest<MsgResponse<List<PharmaceuticalFormListResponseDto>>>;
}