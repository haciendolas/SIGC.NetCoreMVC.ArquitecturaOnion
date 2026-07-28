using MediatR;
using SIGC.DomainModel.Dtos.AttributeValueList;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.AttributeValueFeatures.Queries.AttributeValueList
{
    public sealed record AttributeValueListQueryRequest(bool? AttributeIsVariant
    ) :IRequest<MsgResponse<List<AttributeValueListResponseDto>>>;
}