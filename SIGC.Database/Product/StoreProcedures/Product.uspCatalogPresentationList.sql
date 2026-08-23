 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            16/08/2026
-- Description:            Permite listar presentaciones activas por catalog de la tabla Product.CatalogPresentation
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCatalogPresentationList @CompanyID=1,@CatalogID=1
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogPresentationList(
 @CompanyID INT,
 @CatalogID INT
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT CV.CatalogVariantName,CP.CatalogPresentationID,P.PresentationName AS CatalogPresentationName
	    FROM Product.Presentation P WITH(NOLOCK) 
		INNER JOIN Product.CatalogPresentation CP WITH(NOLOCK) ON P.PresentationID=CP.PresentationID AND P.CompanyID=CP.CompanyID AND CP.RecordStateID=1
		INNER JOIN Product.CatalogVariant CV WITH(NOLOCK) ON CP.CatalogVariantID=CV.CatalogVariantID AND CP.CompanyID=CV.CompanyID AND CV.RecordStateID=1
		WHERE P.CompanyID=@CompanyID 
		AND CV.CatalogID=@CatalogID
		AND P.RecordStateID=1	
		ORDER BY CP.CatalogPresentationIsDefault 
	SET NOCOUNT OFF
END