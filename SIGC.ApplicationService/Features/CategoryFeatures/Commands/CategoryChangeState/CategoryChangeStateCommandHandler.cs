using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Commands.CategoryChangeState
{
    internal class CategoryChangeStateCommandHandler(
        ICategoryChangeStateRepository CategoryChangeStateRepository,
        ICurrentSessionService CurrentSessionService,
        IMessageService MessageService
    ) : IRequestHandler<CategoryChangeStateCommandRequest, MsgResponse<object?>>
    {
        public async Task<MsgResponse<object?>> Handle(CategoryChangeStateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = Category.ChangeState(
                    CurrentSessionService.CompanyID,
                    Request.CategoryId,
                    Request.RecordStateId,
                    DateTime.Now,
                    CurrentSessionService.UserID,
                    CurrentSessionService.UserName,
                    CurrentSessionService.UserFullName
                    );

                var RecordAffected = await CategoryChangeStateRepository.ChangeStateAsync(Model, CancellationToken);
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