 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            19/09/2026
-- Description:            Permite obtener un catalog de la tabla Product.[Catalog]
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCatalogGet  @CompanyID=1 ,@CatalogID=40
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogGet(
   @CompanyID INT,
   @CatalogID INT 
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT C.CatalogID,C.CatalogTypeID,C.CategoryID,C.CatalogCode,C.CatalogSlug,
		C.CatalogName,C.SaleConditionID,C.ManufacturerID,C.BrandID,C.PharmaceuticalFormID,		 
		'TherapeuticActionIDs'=  
	      '[' + ISNULL(STUFF((SELECT ',' , CONVERT(VARCHAR(10), CTA.TherapeuticActionID)
							 FROM Product.CatalogTherapeuticAction CTA WITH(NOLOCK) 
							 WHERE CTA.CatalogID=C.CatalogID AND CTA.CompanyID = C.CompanyID AND CTA.RecordStateID<>2 
							 FOR XML PATH(''), TYPE
					    )
					 .value(N'.[1]', N'varchar(max)'),1,1,''
					)
				,'')
		+']',
		'ActiveIngredientIDs'=  
	      '[' + ISNULL(STUFF((SELECT ',' , CONVERT(VARCHAR(10), CAI.ActiveIngredientID)
							 FROM Product.CatalogActiveIngredient CAI WITH(NOLOCK) 
							 WHERE CAI.CatalogID=C.CatalogID AND CAI.CompanyID = C.CompanyID AND CAI.RecordStateID<>2 
							 FOR XML PATH(''), TYPE
					    )
					 .value(N'.[1]', N'varchar(max)'),1,1,''
					)
				,'')
		+']',
		C.CatalogBrandType,C.CatalogConcentration,C.CatalogHasVariants,C.CatalogDescription,C.CatalogImage,C.RecordStateID		 
		FROM Product.[Catalog] C WITH(NOLOCK)		 
		WHERE C.CompanyID = @CompanyID AND C.CatalogID=@CatalogID 
		AND C.RecordStateID<>2
	SET NOCOUNT OFF
END