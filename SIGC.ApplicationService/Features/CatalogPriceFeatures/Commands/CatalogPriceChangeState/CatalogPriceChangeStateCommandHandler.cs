using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICatalogPriceRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogPriceFeatures.Commands.CatalogPriceChangeState
{
    internal class CatalogPriceChangeStateCommandHandler(
        ICatalogPriceChangeStateRepository CatalogPriceChangeStateRepository,
        ICurrentSessionService CurrentSessionService,
        IMessageService MessageService
    ) : IRequestHandler<CatalogPriceChangeStateCommandRequest, MsgResponse<object?>>
    {
        public async Task<MsgResponse<object?>> Handle(CatalogPriceChangeStateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = CatalogPrice.ChangeState(
                    CurrentSessionService.CompanyID,
                    Request.CatalogPriceID,
                    Request.RecordStateID,
                    DateTime.Now,
                    CurrentSessionService.UserID,
                    CurrentSessionService.UserName,
                    CurrentSessionService.UserFullName
                    );

                var RecordAffected = await CatalogPriceChangeStateRepository.ChangeStateAsync(Model, CancellationToken);
                if (RecordAffected > 0)
                {
                    MsgResponse.Type = MessageTypeConst.SUCCESS;
                    if (Request.RecordStateID == RecordStateEnum.Deleted)
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
            catch(Exception ex)
            {
                MsgResponse.Type = MessageTypeConst.ERROR;
                MsgResponse.Message = $"{MessageService.GetMessageResult(MessageDescriptionConst.ERROR_OPERATION)}:{ex.Message}";
            }
            return MsgResponse;
        }
    }
}