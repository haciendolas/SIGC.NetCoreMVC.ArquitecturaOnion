 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            11/11/2025
-- Description:            Permite obtener un registro de tabla Product.[Catalog]
-- Update:				   Joel Castillo Rojas    
-- Execute                   
/*  
    Exec Product.uspCatalogGetSite @CompanyID=1,@CatalogSlug='lente-semi-oscuro-50-1'
*/
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogGetSite(
   @CompanyID INT,
   @CatalogSlug VARCHAR(200) 
)
AS
BEGIN
  SET NOCOUNT ON 
	 
    SELECT C.CatalogID,C.CatalogName,C.CatalogSlug,C.CatalogDescription,
		  UM.UnitMeasureName,Cat.CategoryName,
		  CASE WHEN C.CatalogDiscount IS NULL THEN 0 ELSE C.CatalogSalePrice END CatalogOldPrice,
		  C.CatalogSalePrice-ISNULL(C.CatalogDiscount,0) AS CatalogCurrentPrice,
		  C.CatalogUnitInStock,
		 'CatalogGallery'=  
	      '[' + ISNULL(STUFF((SELECT ','  + '{'+ 
								 '"GalleryTypeID":' + CONVERT(VARCHAR(10), CG.GalleryTypeID)+','+								 
								  '"CatalogGalleryFileName":"' +ISNULL(CG.CatalogGalleryFileName,'') +'"'+
							 '}'  
							 FROM Product.CatalogGallery CG										 						 
							 WHERE CG.CatalogID=C.CatalogID AND CG.StateID=1 
							 FOR XML PATH(''), TYPE
					    )
						.value(N'.[1]', N'varchar(max)'),1,1,''
					)
				,'')
		+']'
		 
	 FROM Product.[Catalog] C WITH(NOLOCK) 
	     INNER JOIN Product.UnitMeasure UM WITH(NOLOCK) ON C.UnitMeasureID=UM.UnitMeasureID
	     INNER JOIN Product.Category Cat WITH(NOLOCK) ON C.CategoryID=Cat.CategoryID
	  WHERE C.CompanyID=@CompanyID
	       AND ISNULL(C.CatalogSlug,C.CatalogID)=@CatalogSlug 

	SET NOCOUNT OFF
 
END