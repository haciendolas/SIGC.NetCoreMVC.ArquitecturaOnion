using MediatR;
using SIGC.DomainModel.Dtos.PrescriptionType;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PrescriptionTypeFeatures.Queries.PrescriptionTypeList
{
    public sealed record PrescriptionTypeListQueryRequest(        
    ) :IRequest<MsgResponse<List<PrescriptionTypeListResponseDto>>>;
}