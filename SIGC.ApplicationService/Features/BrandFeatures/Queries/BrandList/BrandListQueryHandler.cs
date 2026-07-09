using MediatR;
using SIGC.DomainModel.Dtos.Brand; 
using SIGC.DomainService.IRepositories.IBrandRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.BrandFeatures.Queries.BrandList
{
    internal class BrandListQueryHandler : IRequestHandler<BrandListQueryRequest, MsgResponse<List<BrandListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly IBrandListRepository BrandListRepository;

        public BrandListQueryHandler(
            IMessageService MessageService,   
            IBrandListRepository BrandListRepository
            ) { 
            this.MessageService = MessageService;        
            this.BrandListRepository = BrandListRepository;        
        }

        public async Task<MsgResponse<List<BrandListResponseDto>>> Handle(BrandListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<BrandListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await BrandListRepository.ListAsync(CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
