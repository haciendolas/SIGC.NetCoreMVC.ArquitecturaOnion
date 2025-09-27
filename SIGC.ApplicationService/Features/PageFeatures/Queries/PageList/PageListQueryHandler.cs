using MediatR;
using SIGC.DomainModel.Dtos.Page;
using SIGC.DomainService.IRepositories.IPageRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PageFeatures.Queries.PageList
{
    internal class PageListQueryHandler : IRequestHandler<PageListQueryRequest, MsgResponse<List<PageListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly IPageListRepository PageListRepository;
        public PageListQueryHandler(
            IMessageService MessageService,
            IPageListRepository PageListRepository)
        {
            this.PageListRepository = PageListRepository;
            this.MessageService = MessageService;
        }

        public async Task<MsgResponse<List<PageListResponseDto>>> Handle(PageListQueryRequest request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<PageListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.SUCCESS;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await PageListRepository.ListAsync(CancellationToken);
            if(!MsgResponse.Data.Any()){
               MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}