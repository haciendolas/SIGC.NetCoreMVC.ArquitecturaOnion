namespace SIGC.ApplicationService.Features.CatalogFeatures.Queries.CatalogPagination
{
   public class CatalogPaginationQueryResponse
    {
        public int CatalogID { get; set; }
        public string CatalogName { get; set; } = null!;
        public string CatalogDescription { get; set; } = null!;
        public string CatalogTypeName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string CatalogVariantName { get; set; } = null!;
        public string UnitMeasureName { get; set; } = null!;
        public string PresentationName { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string ManufacturerName { get; set; } = null!;
        public string ActiveIngredient { get; set; } = null!;
        public string TherapeuticAction { get; set; } = null!;
        public byte RecordStateID { get; set; }
        public DateTime CatalogLastUpdatedDateTime { get; set; }
        public string CatalogLastUpdatedUserName { get; set; } = null!;
    }
}