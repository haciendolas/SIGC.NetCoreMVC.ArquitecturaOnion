namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogStock
{
    public sealed class CatalogStockCreateUpdateRequestModel
    {
        public int CatalogStockID { get; set; }
        public int CatalogVariantID { get; set; }
        public int WarehouseID { get; set; }
        public  decimal? CatalogStockCurrentQuantity { get; set; }
        public decimal CatalogStockMinimumQuantity { get; set; }
        public decimal CatalogStockMaximumQuantity { get; set; }   
        public byte RecordOriginID { get; set; }
        public byte RecordStateID { get; set; }
    } 
}