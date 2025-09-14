using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.Role.Queries.RolePagination
{
    public class RolePaginationQueryRequest: PaginationParameters, IRequest<MsgResponse<PaginationResultDto<RolePaginationQueryResponse>>>
    {
        public int CompanyID { get; set; }         
        public short StateID { get; set; }
    }   
}