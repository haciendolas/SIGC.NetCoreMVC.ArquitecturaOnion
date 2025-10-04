using MediatR;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RoleFeatures.Queries.RoleGet
{
    public record struct RoleGetQueryRequest
    (
      int RoleID    
    ):IRequest<MsgResponse<RoleGetQueryResponse?>>;
}