namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category
{
    public sealed record CategoryListResponseModel
    (
        int CategoryID,
        string CategoryName,
        string CategorySlug
     );    
}