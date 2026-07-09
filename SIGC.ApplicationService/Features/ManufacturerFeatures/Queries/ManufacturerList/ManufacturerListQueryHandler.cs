using MediatR;
using SIGC.DomainModel.Dtos.Manufacturer;
using SIGC.DomainService.IRepositories.IManufacturerRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.ManufacturerFeatures.Queries.ManufacturerList
{
    internal class ManufacturerListQueryHandler : IRequestHandler<ManufacturerListQueryRequest, MsgResponse<List<ManufacturerListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly IManufacturerListRepository ManufacturerListRepository;

        public ManufacturerListQueryHandler(
            IMessageService MessageService,
            IManufacturerListRepository ManufacturerListRepository
            ) { 
            this.MessageService = MessageService;        
            this.ManufacturerListRepository = ManufacturerListRepository;        
        }

        public async Task<MsgResponse<List<ManufacturerListResponseDto>>> Handle(ManufacturerListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<ManufacturerListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await ManufacturerListRepository.ListAsync(CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
