using MediatR;
using SIGC.DomainModel.Dtos.Ubigeo;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UbigeoFeatures.Queries.UbigeoListByClassAndCodeAndLenCode
{
    public record struct UbigeoListByClassAndCodeAndLenCodeQueryRequest(
        int UbigeoClass,
        string UbigeoCode,
        int LenUbigeoCode
    ):IRequest<MsgResponse<List<UbigeoListByClassAndCodeAndLenCodeResponseDto>>>;
}