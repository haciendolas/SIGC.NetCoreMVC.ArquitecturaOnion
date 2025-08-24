using MediatR;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.AuthFeatures.Queries.UserLogin
{
    public record struct UserLoginQueryRequest(
            string CompanyDocumentNumber,
            string UserName,
            string UserPassword
        ) : IRequest<MsgResponse<object>>;
}
