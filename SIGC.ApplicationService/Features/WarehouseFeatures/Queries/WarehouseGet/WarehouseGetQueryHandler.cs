using MediatR;
using SIGC.DomainModel.Dtos.Warehouse;
using SIGC.DomainService.IRepositories.IWarehouseRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehouseGet
{
    internal class WarehouseGetQueryHandler : IRequestHandler<WarehouseGetQueryRequest, MsgResponse<WarehouseGetResponseDto?>>
    {        
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService; 
        private readonly IWarehouseGetRepository WarehouseGetRepository;

        public WarehouseGetQueryHandler(            
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IWarehouseGetRepository WarehouseGetRepository
        )
        {         
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;      
            this.WarehouseGetRepository = WarehouseGetRepository;
        }
        public async Task<MsgResponse<WarehouseGetResponseDto?>> Handle(WarehouseGetQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<WarehouseGetResponseDto?>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            var WarehouseGet = await WarehouseGetRepository.GetAsync(CurrentSessionService.CompanyID, Request.WarehouseID, CancellationToken);
            if (WarehouseGet is null)
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            else
            {
                MsgResponse.Data = WarehouseGet;
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            }
            return MsgResponse;
        }
    }
}
