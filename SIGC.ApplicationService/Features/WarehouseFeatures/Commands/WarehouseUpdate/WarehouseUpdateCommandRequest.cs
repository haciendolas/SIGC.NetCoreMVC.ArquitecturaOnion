using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Commands.WarehouseUpdate
{
   public sealed record WarehouseUpdateCommandRequest
   (
      int WarehouseID ,
      int EstablishmentID ,
      byte WarehouseTypeID ,
      string WarehouseCode ,
      string WarehouseName,
      string WarehouseAddress,
      RecordOriginEnum RecordOriginID ,
      RecordStateEnum RecordStateID 
   ): IRequest<MsgResponse<object?>>;
}