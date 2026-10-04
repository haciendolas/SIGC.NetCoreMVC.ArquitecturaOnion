using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPriceFeatures.Commands.CatalogPriceChangeState
{
    public sealed record CatalogPriceChangeStateCommandRequest
    (
      int CatalogPriceID,
      RecordStateEnum RecordStateID
    ) :IRequest<MsgResponse<object?>>;    
}