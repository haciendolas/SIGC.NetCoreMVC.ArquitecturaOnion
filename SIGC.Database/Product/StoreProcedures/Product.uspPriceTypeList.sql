 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            26/07/2026
-- Description:            Permite listar tipo de precios activos de la tabla Product.PriceType
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspPriceTypeList 
-- ============================================================================== 
CREATE PROCEDURE Product.uspPriceTypeList
AS
BEGIN
	SET NOCOUNT ON
		SELECT PT.PriceTypeID,PT.PriceTypeName FROM Product.PriceType PT WITH(NOLOCK) WHERE PT.RecordStateID=1
	SET NOCOUNT OFF
END