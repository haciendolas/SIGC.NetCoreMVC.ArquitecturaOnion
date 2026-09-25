using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogChangeState
{
    public sealed record CatalogChangeStateCommandRequest
    (
      int CatalogID,
      RecordStateEnum RecordStateID
    ) :IRequest<MsgResponse<object?>>;    
}