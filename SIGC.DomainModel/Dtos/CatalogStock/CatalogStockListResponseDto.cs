namespace SIGC.DomainModel.Dtos.CatalogStock
{
    public sealed record CatalogStockListResponseDto
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