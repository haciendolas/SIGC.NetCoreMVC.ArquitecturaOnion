using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainModel.ValueObjects;
using SIGC.DomainService.IRepositories.IUserCompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UserCompanyFeatures.Commands.UserCompanyChangeState
{
    internal class UserCompanyChangeStateCommandHandler : IRequestHandler<UserCompanyChangeStateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IUserCompanyChangeStateRepository UserCompanyChangeStateRepository;
        public UserCompanyChangeStateCommandHandler(
            ICurrentSessionService CurrentSessionService,
             IMessageService MessageService,
            IUserCompanyChangeStateRepository UserCompanyChangeStateRepository  
        )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.UserCompanyChangeStateRepository = UserCompanyChangeStateRepository;
        }

        public async Task<MsgResponse<object?>> Handle(UserCompanyChangeStateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = new UserCompany() { 
                             CompanyID = Request.CompanyID ,
                             UserID = Request.UserID,
                             StateID = Request.StateID,
                             CreatedBy = CurrentSessionService.UserID,
                             CreatedDateTime = DateTime.Now
                            };
            
                var RecordAffected = await UserCompanyChangeStateRepository.ChangeStateAsync(Model, CancellationToken);
                if (RecordAffected > 0)
                {
                    MsgResponse.Type = MessageTypeConst.SUCCESS;
                    if (Request.StateID == RecordStateEnum.Deleted)
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_DELETE);
                    else
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
