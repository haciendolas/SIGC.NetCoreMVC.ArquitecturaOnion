 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            06/07/2026
-- Description:            Permite listar marcas activas de la tabla Product.Brand
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspBrandList
-- ============================================================================== 
CREATE PROCEDURE Product.uspBrandList
AS
BEGIN
	SET NOCOUNT ON
		SELECT B.BrandID,B.BrandName FROM Product.Brand B WHERE B.RecordStateID=1	 
	SET NOCOUNT OFF
END