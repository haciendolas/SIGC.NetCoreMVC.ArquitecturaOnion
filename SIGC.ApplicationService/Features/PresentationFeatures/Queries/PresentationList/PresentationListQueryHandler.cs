using MediatR;
using SIGC.DomainModel.Dtos.Presentation; 
using SIGC.DomainService.IRepositories.IPresentationRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PresentationFeatures.Queries.PresentationList
{
    internal class PresentationListQueryHandler : IRequestHandler<PresentationListQueryRequest, MsgResponse<List<PresentationListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IPresentationListRepository PresentationListRepository;

        public PresentationListQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            IPresentationListRepository PresentationListRepository
            ) { 
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.PresentationListRepository = PresentationListRepository;        
        }

        public async Task<MsgResponse<List<PresentationListResponseDto>>> Handle(PresentationListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<PresentationListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await PresentationListRepository.ListAsync(CurrentSessionService.UserID, Request.UnitMeasureID,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
