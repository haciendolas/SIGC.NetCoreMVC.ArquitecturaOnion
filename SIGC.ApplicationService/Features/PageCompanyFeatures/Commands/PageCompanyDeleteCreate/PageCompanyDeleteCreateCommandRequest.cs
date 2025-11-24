using MediatR;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.PageCompanyFeatures.Commands.PageCompanyCreate
{
    public record struct PageCompanyDeleteCreateCommandRequest
    (
        int CompanyID,
        List<int> PageIDS
    ):IRequest<MsgResponse<object?>>;    
}