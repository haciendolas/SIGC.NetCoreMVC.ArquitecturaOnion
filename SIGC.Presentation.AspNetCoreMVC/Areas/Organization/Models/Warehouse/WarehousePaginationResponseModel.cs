namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Warehouse
{
   public class WarehousePaginationResponseModel
    {
        public int WarehouseID { get; set; }
        public string WarehouseCode { get; set; } = null!;
        public string WarehouseName { get; set; } = null!;
        public string EstablishmentCodeAndName { get; set; } = null!;
        public byte RecordStateID { get; set; }
        public DateTime WarehouseLastUpdatedDateTime { get; set; }
        public int WarehouseLastUpdatedUserID { get; set; }
        public string WarehouseLastUpdatedUserName { get; set; } = null!;
        public string WarehouseLastUpdatedUserFullName { get; set; } = null!;
    }
}