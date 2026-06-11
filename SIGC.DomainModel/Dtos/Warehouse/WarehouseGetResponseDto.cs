namespace SIGC.DomainModel.Dtos.Warehouse
{
    public sealed record WarehouseGetResponseDto
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
