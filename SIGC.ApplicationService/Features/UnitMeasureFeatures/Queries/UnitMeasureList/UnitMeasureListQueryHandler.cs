using MediatR;
using SIGC.DomainModel.Dtos.UnitMeasure;
using SIGC.DomainService.IRepositories.IUnitMeasureRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UnitMeasureFeatures.Queries.UnitMeasureList
{
    internal class UnitMeasureListQueryHandler : IRequestHandler<UnitMeasureListQueryRequest, MsgResponse<List<UnitMeasureListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly IUnitMeasureListRepository UnitMeasureListRepository;

        public UnitMeasureListQueryHandler(
            IMessageService MessageService,   
            IUnitMeasureListRepository UnitMeasureListRepository
            ) { 
            this.MessageService = MessageService;        
            this.UnitMeasureListRepository = UnitMeasureListRepository;        
        }

        public async Task<MsgResponse<List<UnitMeasureListResponseDto>>> Handle(UnitMeasureListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<UnitMeasureListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await UnitMeasureListRepository.ListAsync(38,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
