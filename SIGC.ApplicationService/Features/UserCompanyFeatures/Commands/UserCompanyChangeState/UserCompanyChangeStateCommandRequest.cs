using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UserCompanyFeatures.Commands.UserCompanyChangeState
{
    public record struct UserCompanyChangeStateCommandRequest
    (   int CompanyID,
        int UserID,
        RecordStateEnum StateID
    ): IRequest<MsgResponse<object?>>;
}