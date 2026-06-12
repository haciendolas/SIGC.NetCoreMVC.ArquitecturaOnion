using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IWarehouseRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Commands.WarehouseChangeState
{
    internal class WarehouseChangeStateCommandHandler : IRequestHandler<WarehouseChangeStateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IWarehouseChangeStateRepository WarehouseChangeStateRepository;
        public WarehouseChangeStateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IWarehouseChangeStateRepository WarehouseChangeStateRepository
            )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.WarehouseChangeStateRepository = WarehouseChangeStateRepository;
        }

        public async Task<MsgResponse<object?>> Handle(WarehouseChangeStateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = Warehouse.ChangeState(
                    CurrentSessionService.CompanyID,
                    Request.WarehouseID,
                    Request.RecordStateID,
                    DateTime.Now,
                    CurrentSessionService.UserID,
                    CurrentSessionService.UserName,
                    CurrentSessionService.UserFullName
                    );

                var RecordAffected = await WarehouseChangeStateRepository.ChangeStateAsync(Model, CancellationToken);
                if (RecordAffected > 0)
                {
                    MsgResponse.Type = MessageTypeConst.SUCCESS;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_CHANGE);
                }
                else
                {
                    MsgResponse.Type = MessageTypeConst.ERROR;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_CHANGE);
                }
            }
            catch(Exception ex)
            {
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";
            }
            return MsgResponse;
        }
    }
}