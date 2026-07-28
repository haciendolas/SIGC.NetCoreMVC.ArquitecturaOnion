using MediatR;
using SIGC.DomainModel.Dtos.PriceType;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PriceTypeFeatures.Queries.PriceTypeList
{
    public sealed record PriceTypeListQueryRequest(        
    ) :IRequest<MsgResponse<List<PriceTypeListResponseDto>>>;
}