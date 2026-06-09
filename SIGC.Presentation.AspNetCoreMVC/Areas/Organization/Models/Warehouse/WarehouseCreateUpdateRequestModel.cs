namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Warehouse
{
    public class WarehouseCreateUpdateRequestModel
    {
        public int WarehouseID { get; set; }
        public int EstablishmentID { get; set; }
        public byte WarehouseTypeID { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string WarehouseAddress { get; set; } = null!;
        public byte RecordOriginID { get; set; }
        public byte RecordStateID { get; set; }
    }
}
