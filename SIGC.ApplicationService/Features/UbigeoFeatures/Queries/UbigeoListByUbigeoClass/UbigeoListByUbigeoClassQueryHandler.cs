using MediatR;
using SIGC.DomainModel.Dtos.Ubigeo;
using SIGC.DomainService.IRepositories.IUbigeoRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UbigeoFeatures.Queries.UbigeoListByUbigeoClass
{
    internal class UbigeoListByUbigeoClassQueryHandler : IRequestHandler<UbigeoListByUbigeoClassQueryRequest, MsgResponse<List<UbigeoListByUbigeoClassResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly IUbigeoListByUbigeoClassRepository UbigeoListByUbigeoClassRepository;
        public UbigeoListByUbigeoClassQueryHandler(
            IMessageService MessageService,
            IUbigeoListByUbigeoClassRepository UbigeoListByUbigeoClassRepository)
        {
            this.UbigeoListByUbigeoClassRepository = UbigeoListByUbigeoClassRepository;
            this.MessageService = MessageService;
        }

        public async Task<MsgResponse<List<UbigeoListByUbigeoClassResponseDto>>> Handle(UbigeoListByUbigeoClassQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<UbigeoListByUbigeoClassResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await UbigeoListByUbigeoClassRepository.ListByUbigeoClassAsync(Request.UbigeoClass, CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}