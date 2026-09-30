using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogVariantFeatures.Commands.CatalogVariantChangeState
{
    public sealed record CatalogVariantChangeStateCommandRequest
    (
      int CatalogVariantID,
      RecordStateEnum RecordStateID
    ) :IRequest<MsgResponse<object?>>;    
}