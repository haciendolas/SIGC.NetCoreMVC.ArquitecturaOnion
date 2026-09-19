using MediatR;
using SIGC.DomainModel.Dtos.SaleCondition; 
using SIGC.DomainService.IRepositories.ISaleConditionRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.SaleConditionFeatures.Queries.SaleConditionList
{
    internal class SaleConditionListQueryHandler : IRequestHandler<SaleConditionListQueryRequest, MsgResponse<List<SaleConditionListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly ISaleConditionListRepository SaleConditionListRepository;

        public SaleConditionListQueryHandler(
            IMessageService MessageService,   
            ISaleConditionListRepository SaleConditionListRepository
            ) { 
            this.MessageService = MessageService;        
            this.SaleConditionListRepository = SaleConditionListRepository;        
        }

        public async Task<MsgResponse<List<SaleConditionListResponseDto>>> Handle(SaleConditionListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<SaleConditionListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await SaleConditionListRepository.ListAsync(CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
