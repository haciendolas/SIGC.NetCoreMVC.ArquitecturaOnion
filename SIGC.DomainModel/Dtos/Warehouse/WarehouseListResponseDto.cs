namespace SIGC.DomainModel.Dtos.Warehouse
{
    public sealed record WarehouseListResponseDto
    (
        int WarehouseID,
        string WarehouseCode,
        string WarehouseName
    );
}