using MediatR;
using SIGC.DomainModel.Dtos.Category;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Queries.CategoryGet
{
    internal class CategoryGetQueryHandler(
      ICategoryGetRepository CategoryGetRepository,
      IMessageService MessageService
    ) : IRequestHandler<CategoryGetQueryRequest, MsgResponse<CategoryGetResponseDto?>>
    {
        public async Task<MsgResponse<CategoryGetResponseDto?>> Handle(CategoryGetQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse =new MsgResponse<CategoryGetResponseDto?>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Data = await CategoryGetRepository.GetAsync(Request.CategoryId, CancellationToken);
            if(MsgResponse.Data is null) 
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            else
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);

            return MsgResponse;
        }
    }
}