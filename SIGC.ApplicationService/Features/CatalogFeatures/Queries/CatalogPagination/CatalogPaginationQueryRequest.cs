using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogFeatures.Queries.CatalogPagination
{ 
    public class CatalogPaginationQueryRequest : PaginationParametersDto, IRequest<MsgResponse<PaginationResultDto<CatalogPaginationQueryResponse>>>
    { 
        public byte? CatalogTypeID { get; set; }
        public byte? CategoryID { get; set; }
        public byte? ManufacturerID { get; set; }
        public byte? BrandID { get; set; }
        public byte RecordStateID { get; set; }
    }
}