using MediatR;
using SIGC.DomainModel.Dtos.PageCompany;
using SIGC.DomainService.IRepositories.IPageCompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PageCompanyFeatures.Queries.PageCompanyList
{
    internal class PageCompanyListQueryHandler : IRequestHandler<PageCompanyListQueryRequest, MsgResponse<List<PageCompanyListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly IPageCompanyListRepository PageCompanyListRepository;
        public PageCompanyListQueryHandler(
            IMessageService MessageService,
            IPageCompanyListRepository PageCompanyListRepository
            )
        {
            this.MessageService = MessageService;
            this.PageCompanyListRepository = PageCompanyListRepository;
        }

        public async Task<MsgResponse<List<PageCompanyListResponseDto>>> Handle(PageCompanyListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<PageCompanyListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.SUCCESS;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await PageCompanyListRepository.ListAsync(Request.CompanyID, CancellationToken);
            if(!MsgResponse.Data.Any())
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);

            return MsgResponse;
        }
    }
}
