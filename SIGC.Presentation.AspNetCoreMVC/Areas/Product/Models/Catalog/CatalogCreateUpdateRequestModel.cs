namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Catalog
{
    public sealed class CatalogCreateUpdateRequestModel
    {
        public int CatalogID { get; set; }
        public byte CatalogTypeID { get; set; }
        public int CategoryID { get; set; }
        public string? CatalogCode { get; set; }
        public string CatalogSlug { get; set; } = null!;
        public string CatalogName { get; set; } = null!;
        public byte SaleConditionID { get; set; }
        public int? ManufacturerID { get; set; }
        public int? BrandID { get; set; }
        public short? PharmaceuticalFormID { get; set; }
        public string CatalogBrandType { get; set; } = null!;
        public string? CatalogConcentration { get; set; }
        public string? CatalogDescription { get; set; }
        public bool CatalogHasVariants { get; set; }
        public byte RecordOriginID { get; set; }
        public byte RecordStateID { get; set; } 
        public string? CatalogImage { get; set; }
        public string? CatalogImageBandera { get; set; }
        public IFormFile? FormFile { get; set; }
        public List<CatalogActiveIngredientCreateUpdateRequestModel> CatalogActiveIngredients { get; set; } = new List<CatalogActiveIngredientCreateUpdateRequestModel>();
        public List<CatalogTherapeuticActionCreateUpdateRequestModel> CatalogTherapeuticActions { get; set; } = new List<CatalogTherapeuticActionCreateUpdateRequestModel>();
    }
    public sealed record CatalogActiveIngredientCreateUpdateRequestModel
    (
          int ActiveIngredientID,
          decimal? CatalogActiveIngredientQuantity,
          int? UnitMeasureID,
          string? CatalogActiveIngredientLabel
    );

    public sealed record CatalogTherapeuticActionCreateUpdateRequestModel
     (
         short TherapeuticActionID
     );
}