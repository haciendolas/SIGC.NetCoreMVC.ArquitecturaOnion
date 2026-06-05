 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            17/09/2025
-- Description:            Permite obtener listado paginado de tabla Product.[Catalog]
-- Update:				   Joel Castillo Rojas    
-- Execute                   
/* 
    DECLARE @RecordsTotal INT 
    Exec Product.uspCatalogPaginationSite @CompanyID=1, @CategorySlug='tuberias-pvc', @CatalogName='',@PageNumber=1,@PageSize=100,
	@RecordsTotal=@RecordsTotal OUTPUT

	SELECT @RecordsTotal AS RecordsTotal

*/
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogPaginationSite(
   @CompanyID INT,
   @CategorySlug VARCHAR(100)=NULL,
   @CatalogName VARCHAR(200),    
   @PageNumber INT=1,
   @PageSize INT=10,
   @RecordsTotal INT OUTPUT
)
AS
BEGIN
  SET NOCOUNT ON

    SET @RecordsTotal = (SELECT count(C.CatalogID) FROM Product.[Catalog] C WITH(NOLOCK) WHERE C.CompanyID=@CompanyID AND C.RecordStateID=1 )
	 
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
		+']',
		 COUNT(*) OVER() AS RecordsFiltered
	 FROM Product.[Catalog] C WITH(NOLOCK) 
	     INNER JOIN Product.UnitMeasure UM WITH(NOLOCK) ON C.UnitMeasureID=UM.UnitMeasureID
	     INNER JOIN Product.Category Cat WITH(NOLOCK) ON C.CategoryID=Cat.CategoryID
	  WHERE C.CompanyID=@CompanyID AND C.RecordStateID=1
	       AND Cat.CategorySlug=ISNULL(@CategorySlug,Cat.CategorySlug)
	       AND C.CatalogName LIKE '%'+@CatalogName+'%'
	 ORDER BY C.CatalogID DESC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF
 
END