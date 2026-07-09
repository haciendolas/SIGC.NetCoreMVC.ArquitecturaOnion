using MediatR;
using SIGC.DomainModel.Dtos.Brand;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.BrandFeatures.Queries.BrandList
{
    public sealed record BrandListQueryRequest(        
    ) :IRequest<MsgResponse<List<BrandListResponseDto>>>;
}