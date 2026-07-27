 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            25/07/2026
-- Description:            Permite listar formas farmaceuticas activas de la tabla Product.PharmaceuticalForm
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspPharmaceuticalFormList 
-- ============================================================================== 
ALTER PROCEDURE Product.uspPharmaceuticalFormList
AS
BEGIN
	SET NOCOUNT ON
		SELECT PF.PharmaceuticalFormID,PF.PharmaceuticalFormName FROM Product.PharmaceuticalForm PF WITH(NOLOCK) WHERE PF.RecordStateID=1
	SET NOCOUNT OFF
END