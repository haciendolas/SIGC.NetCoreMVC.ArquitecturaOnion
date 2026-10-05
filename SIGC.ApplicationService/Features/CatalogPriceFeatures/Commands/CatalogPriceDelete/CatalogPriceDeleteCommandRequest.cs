using MediatR;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPriceFeatures.Commands.CatalogPriceDelete
{
    public sealed record CatalogPriceDeleteCommandRequest
    (
      int CatalogPriceID     
    ) :IRequest<MsgResponse<object?>>;    
}