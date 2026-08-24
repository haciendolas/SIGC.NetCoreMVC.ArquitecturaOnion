namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Warehouse
{
    public sealed record WarehouseListResponseModel
    (
        int WarehouseID,
        string WarehouseCode,
        string WarehouseName
    );    
}