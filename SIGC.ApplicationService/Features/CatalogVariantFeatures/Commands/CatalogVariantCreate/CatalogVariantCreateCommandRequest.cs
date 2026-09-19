using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogVariantFeatures.Commands.CatalogVariantCreate
{
    public sealed class CatalogVariantCreateCommandRequest : IRequest<MsgResponse<object?>>
    {  
        public int CatalogID { get; set; }   
        public string CatalogVariantName { get; set; } = null!;
        public string? CatalogVariantSKU { get; set; }        
        public RecordOriginEnum RecordOriginID { get; set; }
        public RecordStateEnum RecordStateID { get; set; } 
        public List<CatalogVariantValueCreateCommandRequest> CatalogVariantValues { get; set; } = new List<CatalogVariantValueCreateCommandRequest>();
        public List<CatalogPresentationCreateCommandRequest> CatalogPresentations { get; set; } = new List<CatalogPresentationCreateCommandRequest>();
    }
}
