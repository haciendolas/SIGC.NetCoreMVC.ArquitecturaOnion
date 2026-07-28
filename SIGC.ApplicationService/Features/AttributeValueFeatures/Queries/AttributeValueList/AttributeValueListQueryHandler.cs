using MediatR;
using SIGC.DomainModel.Dtos.AttributeValueList;
using SIGC.DomainService.IRepositories.IAttributeValueRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.AttributeValueFeatures.Queries.AttributeValueList
{
    internal class AttributeValueListQueryHandler : IRequestHandler<AttributeValueListQueryRequest, MsgResponse<List<AttributeValueListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly IAttributeValueListRepository AttributeValueListRepository;

        public AttributeValueListQueryHandler(
            IMessageService MessageService,
            IAttributeValueListRepository AttributeValueListRepository
            ) { 
            this.MessageService = MessageService;        
            this.AttributeValueListRepository = AttributeValueListRepository;        
        }

        public async Task<MsgResponse<List<AttributeValueListResponseDto>>> Handle(AttributeValueListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<AttributeValueListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await AttributeValueListRepository.ListAsync(Request.AttributeIsVariant,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
