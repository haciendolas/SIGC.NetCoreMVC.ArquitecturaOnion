namespace SIGC.Presentation.AspNetCoreMVC.Areas.Security.Models.Commons
{
    public class PageActionResponseModel
    {        
        public int PageActionID { get; set; }
        public string PageActionName { get; set; } = null!;
        public string PageActionDescription { get; set; } = null!;
    }
}