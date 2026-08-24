using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogCreate
{
    public class CatalogCreateCommandRequest : IRequest<MsgResponse<object>>
    {
        public byte CatalogTypeID { get; set; }
        public int CategoryID { get; set; }
        public string? CatalogCode { get; set; }
        public string CatalogSlug { get; set; } = null!;
        public string CatalogName { get; set; } = null!;
        public byte? PrescriptionTypeID { get; set; }
        public int? ManufacturerID { get; set; }
        public int? BrandID { get; set; }
        public short? PharmaceuticalFormID { get; set; }
        public string CatalogBrandType { get; set; } = null!;
        public string? CatalogDescription { get; set; }       
        public RecordOriginEnum RecordOriginID { get; set; }
        public RecordStateEnum RecordStateID { get; set; }
        public IFileDataService? File { get; set; }
    }
}
