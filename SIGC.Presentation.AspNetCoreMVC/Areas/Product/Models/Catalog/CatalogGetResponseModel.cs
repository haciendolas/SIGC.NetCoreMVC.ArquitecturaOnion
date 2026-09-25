namespace SIGC.Presentation.AspNetCoreMVC.Areas.Product.Models.Catalog
{
    public sealed record CatalogGetResponseModel
    (
          int CatalogID,
          byte CatalogTypeID,
          int CategoryID,
          string CatalogCode,
          string CatalogSlug,
          string CatalogName,
          byte SaleConditionID,
          int ManufacturerID,
          int BrandID,
          short PharmaceuticalFormID,
          string CatalogBrandType,
          string CatalogConcentration,
          bool CatalogHasVariants,
          string CatalogDescription,
          string CatalogImage,
          byte RecordStateID,
          string? CatalogUrl,
          List<short> TherapeuticActionIDs,
          List<int> ActiveIngredientIDs
    );    
}