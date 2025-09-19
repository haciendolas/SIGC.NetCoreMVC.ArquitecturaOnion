using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.RoleFeatures.Queries.RolePagination
{
    public class RolePaginationQueryRequest: PaginationParametersDto, IRequest<MsgResponse<PaginationResultDto<RolePaginationQueryResponse>>>
    {
        public int CompanyID { get; set; }         
        public short StateID { get; set; }
    }   
}