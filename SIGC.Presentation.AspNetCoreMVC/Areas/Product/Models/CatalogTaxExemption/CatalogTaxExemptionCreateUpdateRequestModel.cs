namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTaxExemption
{
    public sealed class CatalogTaxExemptionCreateUpdateRequestModel
    {
        public int CatalogTaxExemptionID { get; set; }
        public int EstablishmentID { get; set; }
        public int CatalogTaxID { get; set; }      
        public byte RecordOriginID { get; set; }
        public byte RecordStateID { get; set; }
    } 
}