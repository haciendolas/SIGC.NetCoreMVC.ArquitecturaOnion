using MediatR;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.AttributeValueFeatures.Queries.AttributeValueList
{
    public sealed record AttributeValueListQueryRequest(bool? AttributeIsVariant
    ) :IRequest<MsgResponse<List<AttributeListQueryResponse>>>;
}