using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IRoleRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RoleFeatures.Commands.RoleChangeState
{
    internal class RoleChangeStateCommandHandler : IRequestHandler<RoleChangeStateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IRoleChangeStateRepository RoleChangeStateRepository;
        public RoleChangeStateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IRoleChangeStateRepository RoleChangeStateRepository
            )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.RoleChangeStateRepository = RoleChangeStateRepository;
        }

        public async Task<MsgResponse<object?>> Handle(RoleChangeStateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = Role.ChangeState(
                         Request.CompanyID,
                         Request.RoleID,
                         Request.StateID,
                         DateTime.Now,
                         CurrentSessionService.UserID
                      );

                var RecordAffected = await RoleChangeStateRepository.ChangeStateAsync(Model, CancellationToken);
                if (RecordAffected > 0)
                {
                    MsgResponse.Type = MessageTypeConst.SUCCESS;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_CHANGE);
                }
                else
                {
                    MsgResponse.Type = MessageTypeConst.ERROR;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_CHANGE);
                }
            }
            catch (Exception ex)
            {
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";
            }
            return MsgResponse;           
        }
    }
}