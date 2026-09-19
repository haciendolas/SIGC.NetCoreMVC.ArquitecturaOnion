 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            26/07/2026
-- Description:            Permite listar las condiciones de ventas activos de la tabla Product.SaleCondition
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspSaleConditionList 
-- ============================================================================== 
ALTER PROCEDURE Product.uspSaleConditionList
AS
BEGIN
	SET NOCOUNT ON
		SELECT SC.SaleConditionID,SC.SaleConditionName FROM Product.SaleCondition SC WITH(NOLOCK) WHERE SC.RecordStateID=1
	SET NOCOUNT OFF
END