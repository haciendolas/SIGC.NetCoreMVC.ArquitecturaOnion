using MediatR;
using SIGC.DomainModel.Dtos.CatalogTaxExemption;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Queries.CatalogTaxExemptionList
{
    public sealed record CatalogTaxExemptionListQueryRequest(
        int CatalogID
    ) :IRequest<MsgResponse<List<CatalogTaxExemptionListResponseDto>>>;
}