using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.AuthFeatures.Queries.AuthLoginToken
{
    public record struct AuthLoginTokenQueryRequest(
            string CompanyDocumentNumber,
            string UserName,
            string UserPassword
    ) : IRequest<MsgResponse<AuthTokenResponseDto>>;
}