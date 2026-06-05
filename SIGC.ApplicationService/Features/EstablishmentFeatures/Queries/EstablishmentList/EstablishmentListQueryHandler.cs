using MediatR;
using SIGC.DomainModel.Dtos.Establishment;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentList
{
    internal class EstablishmentListQueryHandler : IRequestHandler<EstablishmentListQueryRequest, MsgResponse<List<EstablishmentListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IEstablishmentListRepository EstablishmentListRepository;
        public EstablishmentListQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            IEstablishmentListRepository EstablishmentListRepository
            ) { 
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.EstablishmentListRepository = EstablishmentListRepository;        
        }

        public async Task<MsgResponse<List<EstablishmentListResponseDto>>> Handle(EstablishmentListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<EstablishmentListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await EstablishmentListRepository.ListAsync(CurrentSessionService.CompanyID, Request.PersonID.HasValue ? Request.PersonID.Value:CurrentSessionService.CompanyID, CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;

        }
    }
}
