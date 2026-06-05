using SIGC.Presentation.AspNetCoreMVC.Helpers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.User
{
    public class UserPaginationRequestModel: DataTableHelper
    {
        public int CompanyID { get; set; }
        public string? UserFullName { get; set; }
        public string? Search { get; set; }
        public short StateID { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
