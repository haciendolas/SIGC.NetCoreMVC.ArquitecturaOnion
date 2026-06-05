using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.UserFeatures.Queries.UserPagination
{
    public class UserPaginationQueryRequest: PaginationParametersDto, IRequest<MsgResponse<PaginationResultDto<UserPaginationQueryResponse>>>
    {
        public int CompanyID { get; set; }
        public string? UserFullName { get; set; } 
        public short StateID { get; set; }      
    }    
}