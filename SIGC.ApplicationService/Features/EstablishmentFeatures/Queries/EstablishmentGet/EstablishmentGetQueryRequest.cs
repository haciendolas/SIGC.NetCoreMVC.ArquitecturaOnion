using MediatR;
using SIGC.DomainModel.Dtos.Establishment;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Queries.EstablishmentGet
{
   public sealed record EstablishmentGetQueryRequest
   (
        int EstablishmentID
   ):IRequest<MsgResponse<EstablishmentGetResponseDto?>>;
}