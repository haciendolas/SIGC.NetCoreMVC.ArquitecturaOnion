namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogStock
{
    public sealed record CatalogStockListResponseModel
    (
        int CatalogStockID,
        int CatalogVariantID,
        string CatalogVariantName,
        int EstablishmentID,
        string EstablishmentName,
        int WarehouseID,
        string WarehouseCode,
        string WarehouseName,
        decimal CatalogStockMinimumQuantity,
        decimal CatalogStockMaximumQuantity,
        decimal CatalogStockCurrentQuantity
    );    
}