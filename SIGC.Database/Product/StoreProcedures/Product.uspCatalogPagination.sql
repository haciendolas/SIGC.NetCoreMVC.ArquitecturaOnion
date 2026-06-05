 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            17/09/2025
-- Description:            Permite obtener listado paginado de tabla Product.[Catalog]
-- Update:				   Joel Castillo Rojas    
-- Execute                   
/* 
    DECLARE @RecordsTotal INT 
    Exec Product.uspCatalogPagination @CompanyID=1, @CategoryID=1, @CatalogName='',@PageNumber=1,@PageSize=100,
	@RecordsTotal=@RecordsTotal OUTPUT

	SELECT @RecordsTotal AS RecordsTotal 
*/
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogPagination(
   @CompanyID INT,
   @CategoryID INT,
   @CatalogName VARCHAR(200),    
   @PageNumber INT=1,
   @PageSize INT=10,
   @RecordsTotal INT OUTPUT
)
AS
BEGIN
  SET NOCOUNT ON

    SET @RecordsTotal = (SELECT count(C.CatalogID) FROM Product.[Catalog] C WITH(NOLOCK) WHERE C.CompanyID=@CompanyID AND C.RecordStateID<>2)
	 
    SELECT C.CatalogID,C.CatalogName,C.CatalogSlug,C.CatalogDescription,
	      UM.UnitMeasureID,
		  UM.UnitMeasureName,
		  Cat.CategoryID,
		  Cat.CategoryName,
		  C.CatalogDiscount,
		  C.CatalogSalePrice,		  
		  C.CatalogUnitInStock,
		  C.RecordStateID,
		 COUNT(C.CatalogID) OVER() AS RecordsFiltered
	 FROM Product.[Catalog] C WITH(NOLOCK) 
	     INNER JOIN Product.UnitMeasure UM WITH(NOLOCK) ON C.UnitMeasureID=UM.UnitMeasureID
	     INNER JOIN Product.Category Cat WITH(NOLOCK) ON C.CategoryID=Cat.CategoryID
	  WHERE C.CompanyID=@CompanyID AND C.RecordStateID<>2
	       AND (Cat.CategoryID=@CategoryID OR @CategoryID=0)
	       AND C.CatalogName LIKE '%'+@CatalogName+'%'
	 ORDER BY C.CatalogID DESC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF
 
END