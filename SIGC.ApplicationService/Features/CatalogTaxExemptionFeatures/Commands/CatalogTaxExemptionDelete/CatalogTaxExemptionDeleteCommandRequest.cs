using MediatR;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Commands.CatalogTaxExemptionDelete
{
    public sealed record CatalogTaxExemptionDeleteCommandRequest
    (
      int CatalogTaxExemptionID
    ) :IRequest<MsgResponse<object?>>;    
}