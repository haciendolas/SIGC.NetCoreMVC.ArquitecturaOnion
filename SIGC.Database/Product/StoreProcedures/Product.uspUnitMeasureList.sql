 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            27/09/2025
-- Description:            Permite listar la categorias activas de la tabla Product.UnitMeasure
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspUnitMeasureList @CountryID=38
-- ============================================================================== 
ALTER PROCEDURE Product.uspUnitMeasureList
 @CountryID INT 
AS
BEGIN
	SET NOCOUNT ON
		SELECT UM.UnitMeasureID,UM.UnitMeasureCode,UM.UnitMeasureName FROM Product.UnitMeasure UM WITH(NOLOCK) 
		WHERE UM.CountryID=@CountryID AND UM.RecordStateID=1
	SET NOCOUNT OFF
END