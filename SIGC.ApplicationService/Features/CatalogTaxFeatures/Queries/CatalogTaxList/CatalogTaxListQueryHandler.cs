using MediatR;
using SIGC.DomainModel.Dtos.CatalogTax; 
using SIGC.DomainService.IRepositories.ICatalogTaxRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxFeatures.Queries.CatalogTaxList
{
    internal class CatalogTaxListQueryHandler : IRequestHandler<CatalogTaxListQueryRequest, MsgResponse<List<CatalogTaxListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly ICatalogTaxListRepository CatalogTaxListRepository;

        public CatalogTaxListQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            ICatalogTaxListRepository CatalogTaxListRepository
            ) { 
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.CatalogTaxListRepository = CatalogTaxListRepository;        
        }

        public async Task<MsgResponse<List<CatalogTaxListResponseDto>>> Handle(CatalogTaxListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<CatalogTaxListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await CatalogTaxListRepository.ListAsync(CurrentSessionService.CompanyID,Request.CatalogID,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
