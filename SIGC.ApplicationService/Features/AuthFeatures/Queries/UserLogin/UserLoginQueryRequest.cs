using MediatR;

namespace SIGC.ApplicationService.Features.AuthFeatures.Queries.UserLogin
{
    public record struct UserLoginQueryRequest(
            string CompanyDocumentNumber,
            string UserName,
            string UserPassword
        ) : IRequest<string>;
}
