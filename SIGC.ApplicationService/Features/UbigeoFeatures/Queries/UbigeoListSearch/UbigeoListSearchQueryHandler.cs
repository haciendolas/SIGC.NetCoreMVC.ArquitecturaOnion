using MediatR;
using SIGC.DomainModel.Dtos.Ubigeo;
using SIGC.DomainService.IRepositories.IUbigeoRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UbigeoFeatures.Queries.UbigeoListSearch
{
    internal class UbigeoListSearchQueryHandler : IRequestHandler<UbigeoListSearchQueryRequest, MsgResponse<List<UbigeoListSearchResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly IUbigeoListSearchRepository UbigeoListSearchRepository;
        public UbigeoListSearchQueryHandler(
            IMessageService MessageService,
            IUbigeoListSearchRepository UbigeoListSearchRepository)
        {
            this.UbigeoListSearchRepository = UbigeoListSearchRepository;
            this.MessageService = MessageService;
        }

        public async Task<MsgResponse<List<UbigeoListSearchResponseDto>>> Handle(UbigeoListSearchQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<UbigeoListSearchResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await UbigeoListSearchRepository.ListSearchAsync(Request.UbigeoClassContinent,Request.UbigeoName, CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
