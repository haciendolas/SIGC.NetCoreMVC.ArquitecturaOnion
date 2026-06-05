using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Commands.WarehouseCreate
{
   public class WarehouseCreateCommandRequest: IRequest<MsgResponse<object?>>
    {
        public int EstablishmentID { get; set; }
        public byte WarehouseTypeID { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string WarehouseAddress { get; set; } = null!;
        public RecordOriginEnum RecordOriginID { get; set; }
        public RecordStateEnum RecordStateID { get; set; }        
    }
}