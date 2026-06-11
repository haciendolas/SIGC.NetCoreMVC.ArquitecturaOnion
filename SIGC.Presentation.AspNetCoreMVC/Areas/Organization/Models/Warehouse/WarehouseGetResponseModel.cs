namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Warehouse
{
    public sealed record WarehouseGetResponseModel
    (
        int WarehouseID,
        int EstablishmentID,
        byte WarehouseTypeID,
        string WarehouseCode,
        string WarehouseName,
        string WarehouseAddress,
        byte RecordStateID
    );    
}