namespace SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogUpdate
{
    public sealed record CatalogActiveIngredientUpdateCommandRequest
    (
        int CatalogID,
        int ActiveIngredientID,
        decimal? CatalogActiveIngredientQuantity,
        int? UnitMeasureID,
        string? CatalogActiveIngredientLabel
    );    
}