using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogTaxFeatures.Commands.CatalogTaxUpdate
{
    public sealed record CatalogTaxUpdateCommandRequest
    (   int CatalogTaxID,
        int CatalogID,
        short TaxID,
        byte TaxAffectationTypeID,      
        RecordStateEnum RecordStateID
    ) :IRequest<MsgResponse<object>>;
}
