using MediatR;
using SIGC.DomainModel.Dtos.Role;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RoleFeatures.Queries.RoleList
{
    public record struct RoleListQueryRequest
    (
        int CompanyID
    ):IRequest<MsgResponse<List<RoleListResponseDto>>>;
}