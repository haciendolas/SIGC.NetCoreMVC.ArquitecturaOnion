using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxExemptionFeatures.Commands.CatalogTaxExemptionCreate
{
    public sealed record CatalogTaxExemptionCreateCommandRequest
    (       
        int EstablishmentID,
        int CatalogTaxID,        
        RecordOriginEnum RecordOriginID,
        RecordStateEnum RecordStateID 
    ):IRequest<MsgResponse<object>>;
}
