using SIGC.DomainModel.Enums;

namespace SIGC.DomainModel.ValueObjects
{
    public sealed record CatalogActiveIngredient
    (   int CompanyID,
        int CatalogID,
        int ActiveIngredientID,
        decimal? CatalogActiveIngredientQuantity,
        int? UnitMeasureID,
        string? CatalogActiveIngredientLabel,
        RecordOriginEnum RecordOriginID,
        RecordStateEnum RecordStateID,
        int CreatedById,
        string CreatedByName,
        string CreatedByFullName,
        DateTime CreatedDate
    );    
}