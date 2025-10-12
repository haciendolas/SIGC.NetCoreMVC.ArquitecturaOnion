using MediatR;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Queries.CompanyGet
{
    public record struct CompanyGetQueryRequest
    (
      int CompanyID
    ) :IRequest<MsgResponse<CompanyGetQueryResponse?>>;
}