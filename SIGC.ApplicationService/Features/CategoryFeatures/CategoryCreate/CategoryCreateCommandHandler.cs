using MediatR;
using SIGC.DomainModel.Models;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.CategoryCreate
{
    internal class CategoryCreateCommandHandler(
        ICategoryCreateRepository CategoryCreateRepository,
        ICategoryValidateRepository CategoryValidateRepository,
        IMessageService MessageService,
        ICurrentSessionService CurrentSessionService
    ) : IRequestHandler<CategoryCreateCommandRequest, MsgResponse<object>>
    {
        public async Task<MsgResponse<object>> Handle(CategoryCreateCommandRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<object>();
            try{
                var Model = Category.Create(Request.CategoryName, Request.StateId, DateTime.Now, CurrentSessionService.UserID);

                var Validate = await CategoryValidateRepository.ValidateAsync(Model, CancellationToken);
                if (Validate == VerifyRegistryConst.Category.OK)
                {
                    int RecordAffected = await CategoryCreateRepository.CreateAsync(Model, CancellationToken);
                    if (RecordAffected > 0)
                    {
                        MsgResponse.Type = MessageTypeConst.SUCCESS;
                        MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.PROCESS_FULLYCOMPLETED);
                        MsgResponse.Data = new
                        {
                            CategoryId = Model.CategoryId,
                            CategoryName = Model.CategoryName,
                            StateId = Model.StateId,
                            CreatedDate = Model.CreatedDate,
                        };
                    }
                }
                else{
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
