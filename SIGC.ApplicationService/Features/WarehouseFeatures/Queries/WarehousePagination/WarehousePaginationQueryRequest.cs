using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.WarehouseFeatures.Queries.WarehousePagination
{ 
    public class WarehousePaginationQueryRequest : PaginationParametersDto, IRequest<MsgResponse<PaginationResultDto<WarehousePaginationQueryResponse>>>
    {
        public int EstablishmentID { get; set; }
        public byte RecordStateID { get; set; }
    }
}