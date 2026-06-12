using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Commands.WarehouseChangeState
{
    public record struct WarehouseChangeStateCommandRequest
    (
      int WarehouseID,
      RecordStateEnum RecordStateID
    ) :IRequest<MsgResponse<object?>>;    
}