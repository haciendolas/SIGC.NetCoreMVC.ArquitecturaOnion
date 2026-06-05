 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            07/01/2026
-- Description:            Permite obtener una categoria de la tabla Product.Category
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCategoryGet  @CompanyID=1 ,@CategoryID=5
-- ============================================================================== 
ALTER PROCEDURE Product.uspCategoryGet(
   @CompanyID INT,
   @CategoryID INT 
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT C.CategoryID,C.CategoryName,C.CategorySlug,C.CategoryImage,C.RecordStateID		 
		FROM Product.Category C WITH(NOLOCK)		 
		WHERE C.CompanyID = @CompanyID AND C.CategoryID=@CategoryID 
	SET NOCOUNT OFF
END