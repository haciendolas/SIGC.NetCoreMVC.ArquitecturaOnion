using MediatR;
using SIGC.DomainModel.Dtos.AttributeValueList;
using SIGC.DomainService.IRepositories.IAttributeValueRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.AttributeValueFeatures.Queries.AttributeValueList
{
    internal class AttributeValueListQueryHandler : IRequestHandler<AttributeValueListQueryRequest, MsgResponse<List<AttributeListQueryResponse>>>
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

        public async Task<MsgResponse<List<AttributeListQueryResponse>>> Handle(AttributeValueListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<AttributeListQueryResponse>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            var AttributeValueList = await AttributeValueListRepository.ListAsync(Request.AttributeIsVariant,CancellationToken);

            MsgResponse.Data = AttributeValueList.GroupBy(x => new { x.AttributeID, x.AttributeName, x.AttributeIsVariant })
                                .Select(g => new AttributeListQueryResponse(
                                    g.Key.AttributeID,
                                    g.Key.AttributeName,
                                    g.Key.AttributeIsVariant,
                                    g.Select(x => new AttributeValueListQueryResponse(
                                        x.AttributeValueID,
                                        x.AttributeValueName
                                    )).ToList()
                                )).ToList();
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
