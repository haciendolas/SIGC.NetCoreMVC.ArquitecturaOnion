 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            08/07/2026
-- Description:            Permite listar los tipos de catálogos activos de la tabla Product.CatalogType
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCatalogTypeList 
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogTypeList
AS
BEGIN
	SET NOCOUNT ON
		SELECT CT.CatalogTypeID,CT.CatalogTypeName FROM Product.CatalogType CT  WITH(NOLOCK) WHERE CT.RecordStateID=1
	SET NOCOUNT OFF
END