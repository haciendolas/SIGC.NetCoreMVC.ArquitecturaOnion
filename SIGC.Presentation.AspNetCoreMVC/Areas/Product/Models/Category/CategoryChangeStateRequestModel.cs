namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Category
{
    public sealed record CategoryChangeStateRequestModel
    (
         int CategoryId,
         byte RecordStateId
    );
}