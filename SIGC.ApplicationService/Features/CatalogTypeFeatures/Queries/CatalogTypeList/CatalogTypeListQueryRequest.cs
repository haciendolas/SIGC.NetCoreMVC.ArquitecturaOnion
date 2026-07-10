using MediatR;
using SIGC.DomainModel.Dtos.CatalogType;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTypeFeatures.Queries.CatalogTypeList
{
    public sealed record CatalogTypeListQueryRequest(        
    ) :IRequest<MsgResponse<List<CatalogTypeListResponseDto>>>;
}