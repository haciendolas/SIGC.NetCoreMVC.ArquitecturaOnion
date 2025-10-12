using MediatR;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Queries.CompanyGet
{
    internal class CompanyGetQueryHandler : IRequestHandler<CompanyGetQueryRequest, MsgResponse<CompanyGetQueryResponse?>>
    {
        private readonly ICompanyGetRepository CompanyGetRepository;
        private readonly IMessageService MessageService;
        public CompanyGetQueryHandler(ICompanyGetRepository CompanyGetRepository, IMessageService MessageService)
        {
            this.CompanyGetRepository = CompanyGetRepository;
            this.MessageService = MessageService;
        }

        public async Task<MsgResponse<CompanyGetQueryResponse?>> Handle(CompanyGetQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<CompanyGetQueryResponse?>();
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);

            var CompanyGet = await CompanyGetRepository.GetAsync(Request.CompanyID, CancellationToken);
            if(CompanyGet is not  null)
            {
                MsgResponse.Type = MessageTypeConst.QUERY;
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);

                var CompanyResponse = new CompanyGetQueryResponse()
                {
                    CompanyID = CompanyGet.Value.CompanyID,
                    CompanyTradeName = CompanyGet.Value.CompanyTradeName,
                    CompanySocialReason = CompanyGet.Value.CompanySocialReason,
                    CompanyDocumentNumber = CompanyGet.Value.CompanyDocumentNumber,
                    CompanyBirthDate = CompanyGet.Value.CompanyBirthDate,
                    CountryID = CompanyGet.Value.CountryID,
                    CompanyAddress = CompanyGet.Value.CompanyAddress,
                    TaxpayerTypeID = CompanyGet.Value.TaxpayerTypeID,
                    SectorID = CompanyGet.Value.SectorID,
                    CompanyCorporateEmail = CompanyGet.Value.CompanyCorporateEmail,
                    CompanyMobile = CompanyGet.Value.CompanyMobile,
                    CompanyPhone = CompanyGet.Value.CompanyPhone,
                    CompanyLogo = CompanyGet.Value.CompanyLogo,
                    StateID = CompanyGet.Value.StateID,
                    PageCompany = CompanyGet.Value.PageCompany.Select(sel => new CompanyPageGetQueryResponse{
                                  PageID = sel.PageID
                                 }).ToList()
                };
                MsgResponse.Data = CompanyResponse;
            }
            return  MsgResponse;
        }
    }
}
