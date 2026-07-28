using MediatR;
using SIGC.DomainModel.Dtos.PrescriptionType; 
using SIGC.DomainService.IRepositories.IPrescriptionTypeRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PrescriptionTypeFeatures.Queries.PrescriptionTypeList
{
    internal class PrescriptionTypeListQueryHandler : IRequestHandler<PrescriptionTypeListQueryRequest, MsgResponse<List<PrescriptionTypeListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly IPrescriptionTypeListRepository PrescriptionTypeListRepository;

        public PrescriptionTypeListQueryHandler(
            IMessageService MessageService,   
            IPrescriptionTypeListRepository PrescriptionTypeListRepository
            ) { 
            this.MessageService = MessageService;        
            this.PrescriptionTypeListRepository = PrescriptionTypeListRepository;        
        }

        public async Task<MsgResponse<List<PrescriptionTypeListResponseDto>>> Handle(PrescriptionTypeListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<PrescriptionTypeListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await PrescriptionTypeListRepository.ListAsync(CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
