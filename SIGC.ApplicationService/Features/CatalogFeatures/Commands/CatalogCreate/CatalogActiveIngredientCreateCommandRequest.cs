namespace SIGC.ApplicationService.Features.CatalogFeatures.Commands.CatalogCreate
{
    public sealed record CatalogActiveIngredientCreateCommandRequest
    ( 
        int ActiveIngredientID,
        decimal? CatalogActiveIngredientQuantity,
        int? UnitMeasureID,
        string? CatalogActiveIngredientLabel
    );    
}