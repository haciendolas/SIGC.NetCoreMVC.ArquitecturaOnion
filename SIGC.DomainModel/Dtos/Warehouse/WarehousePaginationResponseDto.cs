namespace SIGC.DomainModel.Dtos.Warehouse
{
    public class WarehousePaginationResponseDto
    {
        public int WarehouseID { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string EstablishmentCode { get; set; } = null!;
        public string EstablishmentName { get; set; } = null!;
        public byte RecordStateID { get; set; }
        public DateTime WarehouseLastUpdatedDateTime { get; set; }
        public int WarehouseLastUpdatedUserID { get; set; }
        public string WarehouseLastUpdatedUserName { get; set; } = null!;
        public string WarehouseLastUpdatedUserFullName { get; set; } = null!;
    }
}
