using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.IEstablishmentRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentChangeState
{
    internal class EstablishmentChangeStateCommandHandler : IRequestHandler<EstablishmentChangeStateCommandRequest, MsgResponse<object?>>
    {
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly IMessageService MessageService;
        private readonly IEstablishmentChangeStateRepository EstablishmentChangeStateRepository;
        public EstablishmentChangeStateCommandHandler(
            ICurrentSessionService CurrentSessionService,
            IMessageService MessageService,
            IEstablishmentChangeStateRepository EstablishmentChangeStateRepository
            )
        {
            this.CurrentSessionService = CurrentSessionService;
            this.MessageService = MessageService;
            this.EstablishmentChangeStateRepository = EstablishmentChangeStateRepository;
        }

        public async Task<MsgResponse<object?>> Handle(EstablishmentChangeStateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = Establishment.ChangeState(
                    CurrentSessionService.CompanyID,
                    Request.EstablishmentID,
                    Request.RecordStateID,
                    DateTime.Now,
                    CurrentSessionService.UserID,
                    CurrentSessionService.UserName,
                    CurrentSessionService.UserFullName
                    );

                var RecordAffected = await EstablishmentChangeStateRepository.ChangeStateAsync(Model, CancellationToken);
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
            catch(Exception ex)
            {
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";
            }
            return MsgResponse;
        }
    }
}