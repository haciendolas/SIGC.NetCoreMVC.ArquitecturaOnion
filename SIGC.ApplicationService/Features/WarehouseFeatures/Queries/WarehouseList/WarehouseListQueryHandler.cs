using MediatR;
using SIGC.DomainModel.Dtos.Warehouse; 
using SIGC.DomainService.IRepositories.IWarehouseRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehouseList
{
    internal class WarehouseListQueryHandler : IRequestHandler<WarehouseListQueryRequest, MsgResponse<List<WarehouseListResponseDto>>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;    
        private readonly IWarehouseListRepository WarehouseListRepository;

        public WarehouseListQueryHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,   
            IWarehouseListRepository WarehouseListRepository
            ) {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;        
            this.WarehouseListRepository = WarehouseListRepository;        
        }

        public async Task<MsgResponse<List<WarehouseListResponseDto>>> Handle(WarehouseListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<WarehouseListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await WarehouseListRepository.ListAsync(CurrentSessionService.UserID, Request.EstablishmentID,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
