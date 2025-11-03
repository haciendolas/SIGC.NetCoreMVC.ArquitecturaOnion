using MediatR;
using SIGC.ApplicationService.Commons.Dtos;
using SIGC.DomainModel.Dtos.Pagination;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Queries.CompanyPagination
{
    public class CompanyPaginationQueryRequest: PaginationParametersDto, IRequest<MsgResponse<PaginationResultDto<CompanyPaginationQueryResponse>>>
    {
        public int CompanyIDRegister { get; set; }
        public short? TaxpayerTypeID { get; set; }
        public short? RubroID { get; set; }
        public string? CompanyDocumentNumber { get; set; }
        public string? CompanySocialReason { get; set; }
        public short StateID { get; set; }
    }   
}