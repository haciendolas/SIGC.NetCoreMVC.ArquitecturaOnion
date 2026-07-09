using MediatR;
using SIGC.DomainModel.Dtos.Category;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Queries.CategoryList
{
    internal class CategoryListQueryHandler : IRequestHandler<CategoryListQueryRequest, MsgResponse<List<CategoryListResponseDto>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly ICategoryListRepository CategoryListRepository;
        public CategoryListQueryHandler(
            IMessageService MessageService,
            ICurrentSessionService CurrentSessionService,
            ICategoryListRepository CategoryListRepository
            ) { 
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.CategoryListRepository = CategoryListRepository;        
        }

        public async Task<MsgResponse<List<CategoryListResponseDto>>> Handle(CategoryListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<CategoryListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await CategoryListRepository.ListAsync(CurrentSessionService.CompanyID, CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}