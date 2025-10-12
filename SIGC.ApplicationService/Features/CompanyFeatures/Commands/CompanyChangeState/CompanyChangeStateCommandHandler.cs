using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICompanyRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Commands.CompanyChangeState
{
    internal class CompanyChangeStateCommandHandler : IRequestHandler<CompanyChangeStateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly ICompanyChangeStateRepository CompanyChangeStateRepository;
        public CompanyChangeStateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            ICompanyChangeStateRepository CompanyChangeStateRepository
            )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.CompanyChangeStateRepository = CompanyChangeStateRepository;
        }

        public async Task<MsgResponse<object?>> Handle(CompanyChangeStateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = Company.ChangeState(
                         Request.CompanyID,                         
                         Request.StateID,
                         DateTime.Now,
                         CurrentSessionService.UserID
                      );

                var RecordAffected = await CompanyChangeStateRepository.ChangeStateAsync(Model, CancellationToken);
                if (RecordAffected > 0)
                {
                    MsgResponse.Type = MessageTypeConst.SUCCESS;
                    if (Request.StateID == StateEnum.Deleted)                    
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