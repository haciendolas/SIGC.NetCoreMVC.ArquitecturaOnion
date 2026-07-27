 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            25/07/2026
-- Description:            Permite listar acciones terapeuticas activas de la tabla Product.TherapeuticAction
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspTherapeuticActionList 
-- ============================================================================== 
ALTER PROCEDURE Product.uspTherapeuticActionList
AS
BEGIN
	SET NOCOUNT ON
		SELECT TA.TherapeuticActionID,TA.TherapeuticActionName FROM Product.TherapeuticAction TA WITH(NOLOCK) WHERE TA.RecordStateID=1
	SET NOCOUNT OFF
END