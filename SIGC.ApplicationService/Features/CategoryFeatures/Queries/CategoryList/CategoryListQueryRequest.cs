using MediatR;
using SIGC.DomainModel.Dtos.Category;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Queries.CategoryList
{
    public sealed record CategoryListQueryRequest(       
    ) :IRequest<MsgResponse<List<CategoryListResponseDto>>>;
}