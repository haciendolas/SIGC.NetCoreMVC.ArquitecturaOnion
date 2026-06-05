using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CategoryFeatures.Queries.CategoryPagination
{
   public class CategoryPaginationQueryRequest: PaginationParametersDto, IRequest<MsgResponse<PaginationResultDto<CategoryPaginationQueryResponse>>>
   {
        public byte RecordStateID { get; set; }       
   }
}