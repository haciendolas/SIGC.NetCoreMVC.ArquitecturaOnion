using MediatR;
using SIGC.DomainModel.Dtos.CatalogType; 
using SIGC.DomainService.IRepositories.ICatalogTypeRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTypeFeatures.Queries.CatalogTypeList
{
    internal class CatalogTypeListQueryHandler : IRequestHandler<CatalogTypeListQueryRequest, MsgResponse<List<CatalogTypeListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly ICatalogTypeListRepository CatalogTypeListRepository;

        public CatalogTypeListQueryHandler(
            IMessageService MessageService,
            ICatalogTypeListRepository CatalogTypeListRepository
            ) { 
            this.MessageService = MessageService;        
            this.CatalogTypeListRepository = CatalogTypeListRepository;        
        }

        public async Task<MsgResponse<List<CatalogTypeListResponseDto>>> Handle(CatalogTypeListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<CatalogTypeListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await CatalogTypeListRepository.ListAsync(CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
