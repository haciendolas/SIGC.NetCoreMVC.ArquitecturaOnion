using MediatR;
using SIGC.DomainModel.Dtos.Tax;
using SIGC.DomainService.IRepositories.ITaxRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.TaxFeatures.Queries.TaxList
{
    internal class TaxListQueryHandler : IRequestHandler<TaxListQueryRequest, MsgResponse<List<TaxListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly ITaxListRepository TaxListRepository;

        public TaxListQueryHandler(
            IMessageService MessageService,   
            ITaxListRepository TaxListRepository
            ) { 
            this.MessageService = MessageService;        
            this.TaxListRepository = TaxListRepository;        
        }

        public async Task<MsgResponse<List<TaxListResponseDto>>> Handle(TaxListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<TaxListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await TaxListRepository.ListAsync(38,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
