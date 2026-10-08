using MediatR;
using SIGC.DomainModel.Dtos.CatalogTax;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxFeatures.Queries.CatalogTaxList
{
    public sealed record CatalogTaxListQueryRequest(
        int CatalogID
    ) :IRequest<MsgResponse<List<CatalogTaxListResponseDto>>>;
}