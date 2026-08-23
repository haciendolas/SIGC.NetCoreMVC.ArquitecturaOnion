using MediatR;
using SIGC.DomainModel.Dtos.Tax;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.TaxFeatures.Queries.TaxList
{
    public sealed record TaxListQueryRequest(        
    ) :IRequest<MsgResponse<List<TaxListResponseDto>>>;
}