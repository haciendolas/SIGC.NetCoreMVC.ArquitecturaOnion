using SIGC.Presentation.AspNetCoreMVC.Helpers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Organization.Models.Warehouse
{
   public class WarehousePaginationRequestModel : DataTableHelper
    {
        public int EstablishmentID { get; set; }
        public string? Search { get; set; }
        public byte RecordStateID { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    } 
}