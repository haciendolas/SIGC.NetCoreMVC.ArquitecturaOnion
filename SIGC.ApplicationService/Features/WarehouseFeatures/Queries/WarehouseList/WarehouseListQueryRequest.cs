using MediatR;
using SIGC.DomainModel.Dtos.Warehouse;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehouseList
{
    public sealed record WarehouseListQueryRequest(int EstablishmentID
    ) :IRequest<MsgResponse<List<WarehouseListResponseDto>>>;
}