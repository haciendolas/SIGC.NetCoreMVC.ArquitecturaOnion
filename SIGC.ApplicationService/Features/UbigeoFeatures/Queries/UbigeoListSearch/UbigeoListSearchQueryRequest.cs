using MediatR;
using SIGC.DomainModel.Dtos.Ubigeo;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UbigeoFeatures.Queries.UbigeoListSearch
{
    public record struct UbigeoListSearchQueryRequest(
       int UbigeoClassContinent,
       string UbigeoName
    ):IRequest<MsgResponse<List<UbigeoListSearchResponseDto>>>;
}