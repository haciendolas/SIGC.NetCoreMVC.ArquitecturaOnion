using MediatR;
using SIGC.DomainModel.Dtos.PageCompany;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PageCompanyFeatures.Queries.PageCompanyList
{
    public record struct PageCompanyListQueryRequest
    (
        int CompanyID
    ):IRequest<MsgResponse<List<PageCompanyListResponseDto>>>;    
}
