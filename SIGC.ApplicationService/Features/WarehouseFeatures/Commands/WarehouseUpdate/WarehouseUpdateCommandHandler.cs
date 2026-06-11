using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IWarehouseRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Commands.WarehouseUpdate
{
    internal class WarehouseUpdateCommandHandler : IRequestHandler<WarehouseUpdateCommandRequest, MsgResponse<object?>>
    {       
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;   
        private readonly IWarehouseUpdateRepository WarehouseUpdateRepository;

        public WarehouseUpdateCommandHandler(          
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IWarehouseUpdateRepository WarehouseUpdateRepository
            )
        {           
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;           
            this.WarehouseUpdateRepository = WarehouseUpdateRepository;
        }
        public async Task<MsgResponse<object?>> Handle(WarehouseUpdateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();            
            try
            {
                var CurrentDate = DateTime.Now;
                var Model = Warehouse.Update(
                        CurrentSessionService.CompanyID,
                        Request.WarehouseID,
                        Request.EstablishmentID,
                        Request.WarehouseTypeID,
                        Request.WarehouseCode,
                        Request.WarehouseName,
                        Request.WarehouseAddress,                   
                        Request.RecordStateID,
                        CurrentDate,
                        CurrentSessionService.UserID,
                        CurrentSessionService.UserName,
                        CurrentSessionService.UserFullName
                    );

                var Validate = await WarehouseUpdateRepository.UpdateAsync(Model, CancellationToken);
                if (Validate == VerifyRegistryConst.Warehouse.OK)
                {                    
                    if (Model.WarehouseID > 0)
                    { 
                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            Model.WarehouseID,
                            Model.EstablishmentID,
                            Model.WarehouseCode,
                            Model.WarehouseName,
                            Model.RecordStateID,
                            Model.CreatedDate,
                        };
                    }
                    else
                    {
                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_INSERT);
                    }
                }
                else
                {
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_WAREHOUSE_WAREHOUSENAME);
                }
            }
            catch (ArgumentNullException ae)
            {
                MsgResponse.Type = MessageTypeConst.WARNING;
                MsgResponse.Message = ae.Message;
            }
            catch (Exception ex)
            {
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";
            }

            return MsgResponse;
        }
    }
}