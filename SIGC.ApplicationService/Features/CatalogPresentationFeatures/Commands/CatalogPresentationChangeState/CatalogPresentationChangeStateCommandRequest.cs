using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPresentationFeatures.Commands.CatalogPresentationChangeState
{
    public sealed record CatalogPresentationChangeStateCommandRequest
    (
      int CatalogPresentationID,
      RecordStateEnum RecordStateID
    ) :IRequest<MsgResponse<object?>>;    
}