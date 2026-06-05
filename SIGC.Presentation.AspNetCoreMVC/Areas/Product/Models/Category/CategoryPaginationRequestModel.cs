using SIGC.Presentation.AspNetCoreMVC.Helpers;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category
{
    public class CategoryPaginationRequestModel: DataTableHelper
    {
        public string? Search { get; set; }
        public byte RecordStateID { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
