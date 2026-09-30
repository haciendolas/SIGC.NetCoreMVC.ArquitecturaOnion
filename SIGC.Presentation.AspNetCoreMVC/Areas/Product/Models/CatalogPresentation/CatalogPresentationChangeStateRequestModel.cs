namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.CatalogPresentation
{
    public sealed record CatalogPresentationChangeStateRequestModel
    (
         int CatalogPresentationID,
         byte RecordStateID
    );
}