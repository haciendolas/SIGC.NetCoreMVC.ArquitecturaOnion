using MediatR;
using SIGC.DomainModel.Dtos.ActiveIngredient; 
using SIGC.DomainService.IRepositories.IActiveIngredientRepositories; 
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.ActiveIngredientFeatures.Queries.ActiveIngredientList
{
    internal class ActiveIngredientListQueryHandler : IRequestHandler<ActiveIngredientListQueryRequest, MsgResponse<List<ActiveIngredientListResponseDto>>>
    {
        private readonly IMessageService MessageService;    
        private readonly IActiveIngredientListRepository ActiveIngredientListRepository;

        public ActiveIngredientListQueryHandler(
            IMessageService MessageService,   
            IActiveIngredientListRepository ActiveIngredientListRepository
            ) { 
            this.MessageService = MessageService;        
            this.ActiveIngredientListRepository = ActiveIngredientListRepository;        
        }

        public async Task<MsgResponse<List<ActiveIngredientListResponseDto>>> Handle(ActiveIngredientListQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<List<ActiveIngredientListResponseDto>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            MsgResponse.Data = await ActiveIngredientListRepository.ListAsync(CancellationToken);
            if (!MsgResponse.Data.Any())
            {
                MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);
            }
            return MsgResponse;
        }
    }
}
