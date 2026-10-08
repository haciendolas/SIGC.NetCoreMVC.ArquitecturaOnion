 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            06/10/2026
-- Description:            Permite listar impuesto por catalog de la tabla Product.CatalogTax
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCatalogTaxList @CompanyID=1,@CatalogID=1
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogTaxList(
 @CompanyID INT,
 @CatalogID INT
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT CT.CatalogTaxID,CT.TaxID, E.TaxName,E.TaxValor,
		Ctype.CalculationTypeName,
		CT.TaxAffectationTypeID,Afec.ConstantName AS TaxAffectationTypeName,
		CT.RecordStateID
	    FROM Product.CatalogTax CT  WITH(NOLOCK) 
		INNER JOIN Accounting.Tax E WITH(NOLOCK) ON CT.TaxID=E.TaxID AND E.RecordStateID<>2
		INNER JOIN Accounting.CalculationType CType WITH(NOLOCK) ON E.CalculationTypeID=CType.CalculationTypeID AND CType.RecordStateID<>2
		INNER JOIN [Security].Constant Afec  WITH(NOLOCK) ON CT.TaxAffectationTypeID=Afec.ConstantID AND Afec.ConstantClass=1065 AND Afec.StateID<>2
		WHERE CT.CompanyID=@CompanyID 
		AND CT.CatalogID=@CatalogID
		AND CT.RecordStateID<>2	
		ORDER BY CT.CatalogTaxID 
	SET NOCOUNT OFF
END