using MediatR;
using SIGC.DomainModel.Dtos.TherapeuticAction; 
using SIGC.DomainService.IRepositories.ITherapeuticActionRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.TherapeuticActionFeatures.Queries.TherapeuticActionList
{
    internal class TherapeuticActionListQueryHandler : IRequestHandler<TherapeuticActionListQueryRequest, MsgResponse<List<TherapeuticActionListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly ITherapeuticActionListRepository TherapeuticActionListRepository;

        public TherapeuticActionListQueryHandler(
            IMessageService MessageService,   
            ITherapeuticActionListRepository TherapeuticActionListRepository
            ) { 
            this.MessageService = MessageService;        
            this.TherapeuticActionListRepository = TherapeuticActionListRepository;        
        }

        public async Task<MsgResponse<List<TherapeuticActionListResponseDto>>> Handle(TherapeuticActionListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<TherapeuticActionListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await TherapeuticActionListRepository.ListAsync(CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
