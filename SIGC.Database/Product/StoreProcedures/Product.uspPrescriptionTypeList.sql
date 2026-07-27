 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            26/07/2026
-- Description:            Permite listar los tipos de catálogos activos de la tabla Product.PrescriptionType
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspPrescriptionTypeList 
-- ============================================================================== 
ALTER PROCEDURE Product.uspPrescriptionTypeList
AS
BEGIN
	SET NOCOUNT ON
		SELECT PT.PrescriptionTypeID,PT.PrescriptionTypeName FROM Product.PrescriptionType PT WITH(NOLOCK) WHERE PT.RecordStateID=1
	SET NOCOUNT OFF
END