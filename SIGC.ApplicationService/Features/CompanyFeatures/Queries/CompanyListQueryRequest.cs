using MediatR;
using SIGC.DomainModel.Dtos.Company;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CompanyFeatures.Queries
{
    public record struct CompanyListQueryRequest(
        int CompanyIDRegister
    ):IRequest<MsgResponse<List<CompanyListResponseDto>>>;   
}