using MediatR;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UserCompanyFeatures.Queries.UserCompanyGet
{
    public record struct UserCompanyGetQueryRequest
    (
       int UserID,
       int CompanyID
    ):IRequest<MsgResponse<UserCompanyGetQueryResponse?>>;    
}
