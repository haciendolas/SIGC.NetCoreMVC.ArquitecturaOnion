 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            25/07/2026
-- Description:            Permite listar principio activos con estado activos de la tabla Product.ActiveIngredient
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspActiveIngredientList 
-- ============================================================================== 
CREATE PROCEDURE Product.uspActiveIngredientList
AS
BEGIN
	SET NOCOUNT ON
		SELECT AI.ActiveIngredientID,AI.ActiveIngredientName FROM Product.ActiveIngredient AI WHERE AI.RecordStateID=1
	SET NOCOUNT OFF
END