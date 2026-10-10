using MediatR;
using SIGC.DomainModel.Dtos.CatalogTaxExemption;
using SIGC.DomainService.IRepositories.ICatalogTaxExemptionRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Queries.CatalogTaxExemptionList
{
    internal class CatalogTaxExemptionListQueryHandler : IRequestHandler<CatalogTaxExemptionListQueryRequest, MsgResponse<List<CatalogTaxExemptionListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly ICatalogTaxExemptionListRepository CatalogTaxExemptionListRepository;

        public CatalogTaxExemptionListQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            ICatalogTaxExemptionListRepository CatalogTaxExemptionListRepository
            ) { 
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.CatalogTaxExemptionListRepository = CatalogTaxExemptionListRepository;        
        }

        public async Task<MsgResponse<List<CatalogTaxExemptionListResponseDto>>> Handle(CatalogTaxExemptionListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<CatalogTaxExemptionListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await CatalogTaxExemptionListRepository.ListAsync(CurrentSessionService.CompanyID,Request.CatalogID,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
