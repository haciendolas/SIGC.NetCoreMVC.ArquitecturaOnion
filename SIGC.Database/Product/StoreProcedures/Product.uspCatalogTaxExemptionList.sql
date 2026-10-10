 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            06/10/2026
-- Description:            Permite listar impuesto excluido por catalog de la tabla Product.CatalogTaxExemption
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCatalogTaxExemptionList @CompanyID=1,@CatalogID=1
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogTaxExemptionList(
 @CompanyID INT,
 @CatalogID INT
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT CTE.CatalogTaxExemptionID, CTE.CatalogTaxID,E.EstablishmentName,CTE.RecordStateID
	    FROM CatalogTaxExemption CTE WITH(NOLOCK) 
		INNER JOIN Product.CatalogTax CT  WITH(NOLOCK) ON CTE.CatalogTaxID=CT.CatalogTaxID AND CTE.CompanyID=CT.CompanyID AND CT.RecordStateID<>2
		INNER JOIN Organization.Establishment E WITH(NOLOCK) ON CTE.EstablishmentID=E.EstablishmentID AND CT.CompanyID=E.CompanyID AND CT.CompanyID=E.PersonID AND E.RecordStateID<>2
		WHERE CTE.CompanyID=@CompanyID 
		AND CT.CatalogID=@CatalogID
		AND CTE.RecordStateID<>2	
		ORDER BY CT.CatalogTaxID 
	SET NOCOUNT OFF
END