 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            16/08/2026
-- Description:            Permite listar impuesto por pais activas de la tabla Accounting.Tax
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Accounting.uspTaxList @CountryID=38
-- ============================================================================== 
ALTER PROCEDURE Accounting.uspTaxList(
 @CountryID INT
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT T.TaxID,T.TaxCode, T.TaxName,T.TaxValor FROM Accounting.Tax T  WITH(NOLOCK) WHERE T.CountryID=@CountryID AND T.RecordStateID=1	 
	SET NOCOUNT OFF
END