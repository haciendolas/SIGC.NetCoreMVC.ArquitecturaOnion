namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Warehouse
{
    public sealed record WarehouseChangeStateRequestModel
    (
       int WarehouseID,
       byte RecordStateID
    );    
}