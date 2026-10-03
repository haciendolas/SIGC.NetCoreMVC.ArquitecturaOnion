namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPrice
{
    public sealed class CatalogPriceCreateUpdateRequestModel
    {
        public int CatalogPriceID { get; set; }
        public int CatalogPresentationID { get; set; }
        public  int EstablishmentID { get; set; }
        public byte PriceTypeID { get; set; }
        public byte CurrencyTypeID { get; set; }
        public decimal CatalogPriceAmount { get; set; }
        public bool CatalogPriceIsTaxIncluded { get; set; }
        public byte RecordOriginID { get; set; }
        public byte RecordStateID { get; set; }
    } 
}