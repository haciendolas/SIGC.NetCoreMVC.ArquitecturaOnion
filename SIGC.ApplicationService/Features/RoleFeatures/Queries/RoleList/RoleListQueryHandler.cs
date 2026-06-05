using MediatR;
using SIGC.DomainModel.Dtos.Role;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RoleFeatures.Queries.RoleList
{
    internal class RoleListQueryHandler : IRequestHandler<RoleListQueryRequest, MsgResponse<List<RoleListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly IRoleListRepository RoleListRepository;
        public RoleListQueryHandler(
             IMessageService MessageService,
            IRoleListRepository RoleListRepository
        )
        {
            this.MessageService = MessageService;
            this.RoleListRepository = RoleListRepository;
        }

        public async Task<MsgResponse<List<RoleListResponseDto>>> Handle(RoleListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<RoleListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await RoleListRepository.ListAsync(Request.CompanyID,CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
