using SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Commons;

namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Page
{
    public class PageListResponseModel
    {      
        public int PageID { get; set; }
        public int PageParentID { get; set; }
        public string PageHierarchy { get; set; } = null!;
        public string PageName { get; set; } = null!;
        public string PageIconName { get; set; } = null!;
        public short PageOrder { get; set; }
        public List<PageActionResponseModel> PageAction { get; set; }
    }
}
