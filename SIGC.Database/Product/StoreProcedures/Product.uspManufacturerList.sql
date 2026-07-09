 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            06/07/2026
-- Description:            Permite listar fabricantes activos de la tabla Product.Manufacturer
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspManufacturerList
-- ============================================================================== 
CREATE PROCEDURE Product.uspManufacturerList
AS
BEGIN
	SET NOCOUNT ON
		SELECT M.ManufacturerID,M.ManufacturerName FROM Product.Manufacturer M WHERE M.RecordStateID=1	 
	SET NOCOUNT OFF
END