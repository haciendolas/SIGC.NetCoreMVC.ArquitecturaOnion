using MediatR;
using SIGC.DomainModel.Dtos.PriceType; 
using SIGC.DomainService.IRepositories.IPriceTypeRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PriceTypeFeatures.Queries.PriceTypeList
{
    internal class PriceTypeListQueryHandler : IRequestHandler<PriceTypeListQueryRequest, MsgResponse<List<PriceTypeListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly IPriceTypeListRepository PriceTypeListRepository;

        public PriceTypeListQueryHandler(
            IMessageService MessageService,   
            IPriceTypeListRepository PriceTypeListRepository
            ) { 
            this.MessageService = MessageService;        
            this.PriceTypeListRepository = PriceTypeListRepository;        
        }

        public async Task<MsgResponse<List<PriceTypeListResponseDto>>> Handle(PriceTypeListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<PriceTypeListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await PriceTypeListRepository.ListAsync(CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
