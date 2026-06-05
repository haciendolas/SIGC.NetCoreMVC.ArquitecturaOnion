using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentPagination
{ 
    public class EstablishmentPaginationQueryRequest : PaginationParametersDto, IRequest<MsgResponse<PaginationResultDto<EstablishmentPaginationQueryResponse>>>
    {
        public int? PersonID { get; set; }
        public byte RecordStateID { get; set; }
    }
}