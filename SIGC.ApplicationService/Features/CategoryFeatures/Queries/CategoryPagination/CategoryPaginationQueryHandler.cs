using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Category;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.DomainService.IRepositories.ICategoryRepositories;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Constants;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Queries.CategoryPagination
{
    internal class CategoryPaginationQueryHandler : IRequestHandler<CategoryPaginationQueryRequest, MsgResponse<PaginationResultDto<CategoryPaginationQueryResponse>>>
    {
        private readonly IMessageService MessageService;
        private readonly ICurrentSessionService CurrentSessionService;
        private readonly ICategoryPaginationRepository CategoryPaginationRepository;

        public CategoryPaginationQueryHandler(
             IMessageService MessageService,
              ICurrentSessionService CurrentSessionService,
            ICategoryPaginationRepository CategoryPaginationRepository)
        {
            this.MessageService = MessageService;
            this.CurrentSessionService = CurrentSessionService;
            this.CategoryPaginationRepository= CategoryPaginationRepository;
        }
        public async Task<MsgResponse<PaginationResultDto<CategoryPaginationQueryResponse>>> Handle(CategoryPaginationQueryRequest Request, CancellationToken CancellationToken)
        {
            var MsgResponse = new MsgResponse<PaginationResultDto<CategoryPaginationQueryResponse>>();
            MsgResponse.Type = MessageTypeConst.QUERY;
            MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_RESULT);
            var Response = await CategoryPaginationRepository.PaginationAsync(new CategoryPaginationRequestDto
            (
                CompanyID : CurrentSessionService.CompanyID,
                RecordStateID : Request.RecordStateID,
                Parameters : new PaginationParametersDto()
                {
                    Search = Request.Search ?? "",
                    PageNumber = Request.PageNumber,
                    PageSize = Request.PageSize
                }
            ), CancellationToken);

            if (!Response.Entities.Any()) MsgResponse.Message = MessageService.GetMessageResult(MessageDescriptionConst.QUERY_EMPTY);

            MsgResponse.Data = new PaginationResultDto<CategoryPaginationQueryResponse>();
            MsgResponse.Data.Items = Response.Entities.Select(s => new CategoryPaginationQueryResponse
                                    {
                                        CategoryID = s.CategoryID,
                                        CategoryName = s.CategoryName,
                                        CategorySlug = s.CategorySlug,
                                        RecordStateID = s.RecordStateID,
                                        CategoryLastUpdatedDateTime = s.CategoryLastUpdatedDateTime,
                                        CategoryLastUpdatedUserID = s.CategoryLastUpdatedUserID,
                                        CategoryLastUpdatedUserName = s.CategoryLastUpdatedUserName,
                                        CategoryLastUpdatedUserFullName = s.CategoryLastUpdatedUserFullName,
                                    }).ToList();
            MsgResponse.Data.TotalRecords = Response.Total;
            MsgResponse.Data.RecordsFiltered = Response.Filtered;
            return MsgResponse;
        }
    }
}