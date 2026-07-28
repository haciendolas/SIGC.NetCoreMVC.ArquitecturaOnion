using MediatR;
using SIGC.DomainModel.Dtos.PharmaceuticalForm; 
using SIGC.DomainService.IRepositories.IPharmaceuticalFormRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PharmaceuticalFormFeatures.Queries.PharmaceuticalFormList
{
    internal class PharmaceuticalFormListQueryHandler : IRequestHandler<PharmaceuticalFormListQueryRequest, MsgResponse<List<PharmaceuticalFormListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly IPharmaceuticalFormListRepository PharmaceuticalFormListRepository;

        public PharmaceuticalFormListQueryHandler(
            IMessageService MessageService,   
            IPharmaceuticalFormListRepository PharmaceuticalFormListRepository
            ) { 
            this.MessageService = MessageService;        
            this.PharmaceuticalFormListRepository = PharmaceuticalFormListRepository;        
        }

        public async Task<MsgResponse<List<PharmaceuticalFormListResponseDto>>> Handle(PharmaceuticalFormListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<PharmaceuticalFormListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await PharmaceuticalFormListRepository.ListAsync(CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
