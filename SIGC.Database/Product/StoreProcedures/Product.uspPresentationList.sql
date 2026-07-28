 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            06/07/2026
-- Description:            Permite listar presentaciones activas de la tabla Product.Presentation
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspPresentationList @CompanyID=1,@UnitMeasureID=1
-- ============================================================================== 
ALTER PROCEDURE Product.uspPresentationList(
 @CompanyID INT,
 @UnitMeasureID INT
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT P.PresentationID,P.PresentationName,P.PresentationEquivalence
	    FROM Product.Presentation P WITH(NOLOCK) 
		WHERE P.CompanyID=@CompanyID 
		AND P.UnitMeasureID=@UnitMeasureID
		AND P.RecordStateID=1	 
	SET NOCOUNT OFF
END