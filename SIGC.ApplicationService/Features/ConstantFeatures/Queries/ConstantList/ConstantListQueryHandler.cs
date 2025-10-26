using MediatR;
using SIGC.DomainModel.Dtos.Constant;
using SIGC.DomainService.IRepositories.IConstantRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.ConstantFeatures.Queries.ConstantList
{
    internal class ConstantListQueryHandler : IRequestHandler<ConstantListQueryRequest, MsgResponse<List<ConstantListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly IConstantListRepository ConstantListRepository;
        public ConstantListQueryHandler(
            IMessageService MessageService,
            IConstantListRepository ConstantListRepository)
        {
            this.ConstantListRepository = ConstantListRepository;
            this.MessageService = MessageService;
        }

        public async Task<MsgResponse<List<ConstantListResponseDto>>> Handle(ConstantListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<ConstantListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await ConstantListRepository.ListAsync(Request.ConstantClass,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}