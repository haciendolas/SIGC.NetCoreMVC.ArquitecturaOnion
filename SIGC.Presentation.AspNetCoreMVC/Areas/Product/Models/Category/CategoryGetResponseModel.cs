namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category
{
    public sealed record CategoryGetResponseModel
    (
         int CategoryId,
         string CategoryName,
         string CategorySlug,
         string CategoryImage,
         byte RecordStateID,
         string CategoryUrl
    );
}