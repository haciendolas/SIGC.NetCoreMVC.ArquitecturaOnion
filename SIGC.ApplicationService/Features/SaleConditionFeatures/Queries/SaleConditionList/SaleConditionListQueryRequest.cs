using MediatR;
using SIGC.DomainModel.Dtos.SaleCondition;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.SaleConditionFeatures.Queries.SaleConditionList
{
    public sealed record SaleConditionListQueryRequest(        
    ) :IRequest<MsgResponse<List<SaleConditionListResponseDto>>>;
}