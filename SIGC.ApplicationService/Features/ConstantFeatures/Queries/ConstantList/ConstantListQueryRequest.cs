using MediatR;
using SIGC.DomainModel.Dtos.Constant;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.ConstantFeatures.Queries.ConstantList
{
    public record struct ConstantListQueryRequest
    (
      string ConstantClass
    ):IRequest<MsgResponse<List<ConstantListResponseDto>>>;
}