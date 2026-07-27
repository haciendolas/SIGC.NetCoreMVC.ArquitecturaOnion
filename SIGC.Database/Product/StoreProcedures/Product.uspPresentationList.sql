 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            06/07/2026
-- Description:            Permite listar presentaciones activas de la tabla Product.Presentation
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspPresentationList
-- ============================================================================== 
CREATE PROCEDURE Product.uspPresentationList
AS
BEGIN
	SET NOCOUNT ON
		SELECT P.PresentationID,P.PresentationName,P.PresentationEquivalence
	    FROM Product.Presentation P WITH(NOLOCK) WHERE P.RecordStateID=1	 
	SET NOCOUNT OFF
END