using MediatR;
using SIGC.DomainModel.Enums;
using SIGC.DomainService.IServices;
using SIGC.Infrastructure.CrossCutting.Wrappers;

namespace SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogUpdate
{
    public class CatalogUpdateCommandRequest : IRequest<MsgResponse<object?>>
    {
        public int CatalogID { get; set; }
        public byte CatalogTypeID { get; set; }
        public int CategoryID { get; set; }
        public string? CatalogCode { get; set; }
        public string CatalogSlug { get; set; } = null!;
        public string CatalogName { get; set; } = null!;
        public bool CatalogHasVariants { get; set; }
        public byte SaleConditionID { get; set; }
        public int? ManufacturerID { get; set; }
        public int? BrandID { get; set; }
        public short? PharmaceuticalFormID { get; set; }
        public string CatalogBrandType { get; set; } = null!;
        public string? CatalogConcentration { get; set; }
        public string? CatalogDescription { get; set; }
        public RecordOriginEnum RecordOriginID { get; set; }
        public RecordStateEnum RecordStateID { get; set; }
        public IFileDataService? File { get; set; }
        public string? CatalogImage { get; set; }
        public string? CatalogImageBandera { get; set; }
        public List<CatalogActiveIngredientUpdateCommandRequest> CatalogActiveIngredients { get; set; } = new List<CatalogActiveIngredientUpdateCommandRequest>();
        public List<CatalogTherapeuticActionUpdateCommandRequest> CatalogTherapeuticActions { get; set; } = new List<CatalogTherapeuticActionUpdateCommandRequest>();
    }
}