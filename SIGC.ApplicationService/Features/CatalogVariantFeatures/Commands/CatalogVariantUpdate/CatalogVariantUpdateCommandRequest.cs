using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogVariantFeatures.Commands.CatalogVariantUpdate
{
    public class CatalogVariantUpdateCommandRequest : IRequest<MsgResponse<object?>>
    {
        public int CatalogVariantID { get; set; }
        public int CatalogID { get; set; }   
        public string CatalogVariantName { get; set; } = null!;
        public string? CatalogVariantSKU { get; set; }        
        public RecordOriginEnum RecordOriginID { get; set; }
        public RecordStateEnum RecordStateID { get; set; } 
        public IReadOnlyList<CatalogVariantValueUpdateCommandRequest> CatalogVariantValues { get; set; } = new List<CatalogVariantValueUpdateCommandRequest>();
        public IReadOnlyList<CatalogPresentationUpdateCommandRequest> CatalogPresentations { get; set; } = new List<CatalogPresentationUpdateCommandRequest>();
    }
}
