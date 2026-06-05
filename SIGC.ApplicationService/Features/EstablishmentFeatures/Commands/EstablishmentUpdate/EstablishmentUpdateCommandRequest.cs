using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.EstablishmentFeatures.Commands.EstablishmentUpdate
{
    public class EstablishmentUpdateCommandRequest : IRequest<MsgResponse<object?>>
    {
        public int EstablishmentID { get; set; }
        public int? PersonID { get; set; }
        public byte TypeID { get; set; }
        public string EstablishmentCode { get; set; } = null!;
        public string EstablishmentName { get; set; } = null!;
        public string EstablishmentAddress { get; set; } = null!;   
        public RecordStateEnum RecordStateId { get; set; }
        public IFileDataService? File { get; set; } 
        public string? EstablishmentLogo { get; set; }
        public string? EstablishmentLogoBandera { get; set; }
    }
}