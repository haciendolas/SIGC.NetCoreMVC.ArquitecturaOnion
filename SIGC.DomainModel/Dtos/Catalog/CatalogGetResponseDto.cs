namespace SIGC.DomainModel.Dtos.Catalog
{
    public sealed record CatalogGetResponseDto
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
      List<short> TherapeuticActionIDs,
      List<int> ActiveIngredientIDs,
      string CatalogBrandType,
      string CatalogConcentration,
      bool CatalogHasVariants,
      string CatalogDescription,
      string CatalogImage,   
      byte RecordStateID, 
      string? CatalogUrl
    );
}