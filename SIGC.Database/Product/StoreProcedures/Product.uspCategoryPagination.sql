 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            23/09/2025
-- Description:            Permite obtener listado paginado de tabla Product.Category
-- Update:				   Joel Castillo Rojas    
-- Execute                   
/* 
    DECLARE @RecordsTotal INT 
    Exec Product.uspCategoryPagination @CompanyID=1, @CategoryName=NULL,@RecordStateID=10,@PageNumber=1,@PageSize=100,
	@RecordsTotal=@RecordsTotal OUTPUT

	SELECT @RecordsTotal AS RecordsTotal

*/
-- ============================================================================== 
ALTER PROCEDURE Product.uspCategoryPagination(
   @CompanyID INT,  
   @CategoryName VARCHAR(200)='',    
   @RecordStateID TINYINT = 10,
   @PageNumber INT= 1,
   @PageSize INT = 10,
   @RecordsTotal INT OUTPUT
)
AS
BEGIN
  SET NOCOUNT ON
    
	IF @PageNumber < 1 SET @PageNumber = 1;
    IF @PageSize <= 0 SET @PageSize = 10;

    SET @RecordsTotal = (SELECT count(C.CategoryID) FROM Product.Category C WITH(NOLOCK) WHERE C.RecordStateID<>2 AND C.CompanyID=@CompanyID)
	                                         
    SELECT C.CategoryID,C.CategoryName,C.CategorySlug,C.RecordStateID,
	       ISNULL(C.CategoryUpdatedDateTime,C.CategoryCreatedDateTime) AS CategoryLastUpdatedDateTime,
		   ISNULL(C.CategoryUpdatedUserID,C.CategoryCreatedUserID) AS CategoryLastUpdatedUserID,
		   ISNULL(C.CategoryUpdatedUserName,C.CategoryCreatedUserName) AS CategoryLastUpdatedUserName,
		   ISNULL(C.CategoryUpdatedUserFullName,C.CategoryCreatedUserFullName) AS CategoryLastUpdatedUserFullName,
	       COUNT(*) OVER() AS RecordsFiltered
	 FROM Product.Category C WITH(NOLOCK)  
	  WHERE C.RecordStateID<>2	      
		   AND ((@RecordStateID IS NULL OR @RecordStateID=10) OR C.RecordStateID=@RecordStateID)
	       AND C.CompanyID=@CompanyID	       
	       AND ((@CategoryName IS NULL OR @CategoryName='') OR C.CategoryName LIKE '%'+@CategoryName+'%')
	 ORDER BY C.CategoryID DESC OFFSET ((@PageNumber-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY 

	SET NOCOUNT OFF
 
END