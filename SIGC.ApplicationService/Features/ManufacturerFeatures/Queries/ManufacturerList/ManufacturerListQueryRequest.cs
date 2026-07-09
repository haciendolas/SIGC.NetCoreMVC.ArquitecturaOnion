using MediatR;
using SIGC.DomainModel.Dtos.Manufacturer;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.ManufacturerFeatures.Queries.ManufacturerList
{
    public sealed record ManufacturerListQueryRequest(        
    ) :IRequest<MsgResponse<List<ManufacturerListResponseDto>>>;
}