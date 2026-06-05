 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            28/10/2025
-- Description:            Permite listar los tipos de galerias activas de la tabla Product.GalleryType
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspGalleryTypeList 
-- ============================================================================== 
CREATE PROCEDURE Product.uspGalleryTypeList
AS
BEGIN
	SET NOCOUNT ON
		SELECT GT.GalleryTypeID,GT.GalleryTypeName FROM Product.GalleryType GT WHERE GT.StateID=1
	SET NOCOUNT OFF
END