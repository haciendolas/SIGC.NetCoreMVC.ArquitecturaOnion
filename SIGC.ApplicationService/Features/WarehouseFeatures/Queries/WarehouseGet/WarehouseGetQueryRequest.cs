using MediatR;
using SIGC.DomainModel.Dtos.Warehouse;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehouseGet
{
   public sealed record WarehouseGetQueryRequest
   (
        int WarehouseID
   ) :IRequest<MsgResponse<WarehouseGetResponseDto?>>;
}