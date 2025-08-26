using MediatR;
using SIGC.DomainModel.Dtos.Category;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Queries.CategoryGet
{
    public record struct CategoryGetQueryRequest(
       int CategoryId
    ):IRequest<MsgResponse<CategoryGetResponseDto?>>;    
}