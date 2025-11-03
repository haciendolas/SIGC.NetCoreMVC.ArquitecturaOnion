using SIGC.Presentation.AspNetCoreMVC.Helpers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Company
{
    public class CompanyPaginationRequestModel: DataTableHelper
    {
        public int CompanyIDRegister { get; set; }
        public short? TaxpayerTypeID { get; set; }
        public short? RubroID { get; set; }
        public string? CompanyDocumentNumber { get; set; }
        public string? CompanySocialReason { get; set; }
        public short StateID { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
