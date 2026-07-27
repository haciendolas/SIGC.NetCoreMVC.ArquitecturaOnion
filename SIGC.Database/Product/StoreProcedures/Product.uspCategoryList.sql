 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            08/09/2025
-- Description:            Permite listar la categorias activas de la tabla Product.Category
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCategoryList @CompanyID=1
-- ============================================================================== 
ALTER PROCEDURE Product.uspCategoryList
 @CompanyID INT
AS
BEGIN
	SET NOCOUNT ON
		SELECT C.CategoryID,C.CategoryName,CategorySlug FROM Product.Category C  WITH(NOLOCK) WHERE C.RecordStateID=1
		AND C.CompanyID=@CompanyID
	SET NOCOUNT OFF
END