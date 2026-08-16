namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Presentation
{
    public sealed record PresentationListResponseModel
    (
        int PresentationID,
        string PresentationName,
        decimal PresentationEquivalence
    );    
}