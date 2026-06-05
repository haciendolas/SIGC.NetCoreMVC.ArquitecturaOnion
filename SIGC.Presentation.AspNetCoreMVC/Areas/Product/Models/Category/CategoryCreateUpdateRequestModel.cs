namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category
{
    public class CategoryCreateUpdateRequestModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string CategorySlug { get; set; } = null!;
        public byte RecordOriginId { get; set; }
        public byte RecordStateId { get; set; }
        public IFormFile? FormFile { get; set; }
        public string? CategoryImage { get; set; }
        public string? CategoryImageBandera { get; set; }
    }
}