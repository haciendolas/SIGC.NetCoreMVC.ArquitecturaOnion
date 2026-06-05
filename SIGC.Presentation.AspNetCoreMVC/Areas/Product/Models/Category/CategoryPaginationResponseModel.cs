namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category
{
    public class CategoryPaginationResponseModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = null!;
        public string CategorySlug { get; set; } = null!;
        public int RecordStateID { get; set; }
        public DateTime CategoryLastUpdatedDateTime { get; set; }
        public int CategoryLastUpdatedUserID { get; set; }
        public string CategoryLastUpdatedUserName { get; set; } = null!;
        public string CategoryLastUpdatedUserFullName { get; set; } = null!;
    }
}
