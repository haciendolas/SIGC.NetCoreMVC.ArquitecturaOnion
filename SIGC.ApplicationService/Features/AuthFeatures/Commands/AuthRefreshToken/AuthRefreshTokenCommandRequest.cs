using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.AuthFeatures.Commands.AuthRefreshToken
{
    public record struct AuthRefreshTokenCommandRequest
    ( string AccessToken,
      string RefreshToken
    ):IRequest<MsgResponse<AuthTokenResponseDto>>;
}