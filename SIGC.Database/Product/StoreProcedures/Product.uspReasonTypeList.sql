 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            26/07/2026
-- Description:            Permite listar tipo de precios activos de la tabla Product.ReasonType
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspReasonTypeList 
-- ============================================================================== 
CREATE PROCEDURE Product.uspReasonTypeList
AS
BEGIN
	SET NOCOUNT ON
		SELECT PT.ReasonTypeID,PT.ReasonTypeName FROM Product.ReasonType PT WITH(NOLOCK) WHERE PT.RecordStateID=1
	SET NOCOUNT OFF
END