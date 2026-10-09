using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxFeatures.Commands.CatalogTaxChangeState
{
    public sealed record CatalogTaxChangeStateCommandRequest
    (
      int CatalogTaxID,
      RecordStateEnum RecordStateID
    ) :IRequest<MsgResponse<object?>>;    
}