using MediatR;
using SIGC.DomainModel.Dtos.Catalog;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogFeatures.Queries.CatalogGet
{
   public sealed record CatalogGetQueryRequest
   (
        int CatalogID
   ):IRequest<MsgResponse<CatalogGetResponseDto?>>;
}