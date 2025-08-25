using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.CategoryUpdate
{
    internal class CategoryUpdateCommandHandler(
        ICategoryUpdateRepository CategoryUpdateRepository,
        ICategoryValidateRepository CategoryValidateRepository,
        ICurrentSessionService CurrentSessionService,
        IMessageService MessageService
     ) : IRequestHandler<CategoryUpdateCommandRequest, MsgResponse<object?>>
     {
        public async Task<MsgResponse<object?>> Handle(CategoryUpdateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object?>();
            try
            {
                var Model = Category.Update(
                    Request.CategoryId,
                    Request.CategoryName,
                    Request.StateId,
                    DateTime.Now,
                    CurrentSessionService.UserID
                    );

                var Validate = await CategoryValidateRepository.ValidateAsync(Model,CancellationToken);
                if (Validate == VerifyRegistryConst.Category.OK)
                {
                    var RecordAffected = await CategoryUpdateRepository.UpdateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {
                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.SATISFACTORY_UPDATE);
                    }
                    else
                    {
                        MsgResponse.Type = MessageTypeConst.ERROR;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.ERROR_UPDATE);
                    }
                }
                else
                {
                    MsgResponse.Type = MessageTypeConst.WARNING;
                    MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.EXIST_CATEGORY_CATEGORYNAME);
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