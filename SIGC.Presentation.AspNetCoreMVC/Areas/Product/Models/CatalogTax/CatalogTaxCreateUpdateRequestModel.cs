namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogTax
{
    public sealed class CatalogTaxCreateUpdateRequestModel
    {
        public int CatalogTaxID { get; set; }
        public int CatalogID { get; set; }
        public  short TaxID { get; set; }
        public byte TaxAffectationTypeID { get; set; }      
        public byte RecordOriginID { get; set; }
        public byte RecordStateID { get; set; }
    } 
}