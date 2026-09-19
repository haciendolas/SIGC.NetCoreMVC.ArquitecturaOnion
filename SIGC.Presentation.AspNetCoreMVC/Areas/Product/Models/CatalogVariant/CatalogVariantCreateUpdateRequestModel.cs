namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogVariant
{
    public sealed class CatalogVariantCreateUpdateRequestModel
    {
        public int CatalogVariantID { get; set; }
        public int CatalogID { get; set; }
        public string CatalogVariantName { get; set; } = null!;
        public string? CatalogVariantSKU { get; set; }
        public byte RecordOriginID { get; set; }
        public byte RecordStateID { get; set; }
        public List<CatalogVariantValueCreateUpdateRequestModel> CatalogVariantValues { get; set; } = new List<CatalogVariantValueCreateUpdateRequestModel>();
        public List<CatalogPresentationCreateUpdateRequestModel> CatalogPresentations { get; set; } = new List<CatalogPresentationCreateUpdateRequestModel>();
    }

    public sealed record CatalogVariantValueCreateUpdateRequestModel
    (
         short AttributeValueID
    );

    public sealed record CatalogPresentationCreateUpdateRequestModel
        (
            int CatalogPresentationID,
            int PresentationID,
            bool CatalogPresentationIsDefault,
            decimal CatalogPresentationEquivalence,
            string? CatalogPresentationSKU,
            string? CatalogPresentationBarcode,
            byte RecordStateID
        );
}
