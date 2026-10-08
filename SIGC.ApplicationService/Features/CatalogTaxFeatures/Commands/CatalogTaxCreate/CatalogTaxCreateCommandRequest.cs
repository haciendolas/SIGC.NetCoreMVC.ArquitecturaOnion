using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxFeatures.Commands.CatalogTaxCreate
{
    public sealed record CatalogTaxCreateCommandRequest
    (
        int CatalogID,
        short TaxID,
        byte TaxAffectationTypeID,
        RecordOriginEnum RecordOriginID,
        RecordStateEnum RecordStateID 
    ):IRequest<MsgResponse<object>>;
}
