namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPresentation
{
    public sealed class CatalogPresentationCreateRequestModel
    {       
        public int CatalogVariantID { get; set; }
        public int PresentationID { get; set; }
        public bool CatalogPresentationIsDefault { get; set; }
        public decimal CatalogPresentationEquivalence { get; set; }
        public string? CatalogPresentationSKU { get; set; }
        public string? CatalogPresentationBarcode { get; set; }
        public byte RecordOriginID { get; set; }
        public byte RecordStateID { get; set; }
    }
}