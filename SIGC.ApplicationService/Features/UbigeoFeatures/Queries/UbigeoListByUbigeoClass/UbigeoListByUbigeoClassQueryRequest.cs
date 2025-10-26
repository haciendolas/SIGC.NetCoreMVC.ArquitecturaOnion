using MediatR;
using SIGC.DomainModel.Dtos.Ubigeo;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UbigeoFeatures.Queries.UbigeoListByUbigeoClass
{
    public record struct UbigeoListByUbigeoClassQueryRequest(
        int UbigeoClass
    ):IRequest<MsgResponse<List<UbigeoListByUbigeoClassResponseDto>>>;
}