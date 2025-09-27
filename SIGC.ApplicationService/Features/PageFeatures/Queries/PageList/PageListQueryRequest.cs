using MediatR;
using SIGC.DomainModel.Dtos.Page;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PageFeatures.Queries.PageList
{
    public record struct PageListQueryRequest():IRequest<MsgResponse<List<PageListResponseDto>>>;
}
