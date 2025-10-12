using MediatR;
using SIGC.DomainModel.Dtos.Company;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Queries.CompanyList
{
    public record struct CompanyListQueryRequest(
        int CompanyIDRegister
    ):IRequest<MsgResponse<List<CompanyListResponseDto>>>;   
}