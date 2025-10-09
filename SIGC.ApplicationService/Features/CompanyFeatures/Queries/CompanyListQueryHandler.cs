using MediatR;
using SIGC.DomainModel.Dtos.Company;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Queries
{
    internal class CompanyListQueryHandler : IRequestHandler<CompanyListQueryRequest, MsgResponse<List<CompanyListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICompanyListRepository CompanyListRepository;
        public CompanyListQueryHandler(
            IMessageService MessageService,
            ICompanyListRepository CompanyListRepository)
        {
            this.MessageService = MessageService;
            this.CompanyListRepository = CompanyListRepository;           
        }
        public async Task<MsgResponse<List<CompanyListResponseDto>>> Handle(CompanyListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<CompanyListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await CompanyListRepository.ListAsync(Request.CompanyIDRegister,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}