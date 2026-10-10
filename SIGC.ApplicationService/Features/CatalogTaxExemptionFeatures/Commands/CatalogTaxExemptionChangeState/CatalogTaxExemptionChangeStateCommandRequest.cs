using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Commands.CatalogTaxExemptionChangeState
{
    public sealed record CatalogTaxExemptionChangeStateCommandRequest
    (
      int CatalogTaxExemptionID,
      RecordStateEnum RecordStateID
    ) :IRequest<MsgResponse<object?>>;    
}