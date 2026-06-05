 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            11/12/2025
-- Description:            Permite obtener un registro con su detalle de tabla Sale.Document y Sale.DocumentItem 
-- Update:				   Joel Castillo Rojas    
-- Execute                   
 
-- Exec Sale.uspDocumentGet @DocumentID=1
 
-- ============================================================================== 
ALTER PROCEDURE Sale.uspDocumentGet(
   @DocumentID BIGINT 
)
AS
BEGIN
  SET NOCOUNT ON 
	 SELECT 
	  D.DocumentCode,D.DocumentCorrelative,D.CustomerFullName,
	  D.CustomerAddress,D.CustomerMobilePhone,
	  CONVERT(DATETIME,D.DocumentIssueDate)+CONVERT(DATETIME,D.DocumentIssueTime) AS DocumentIssueDateTime,
	  D.DocumentExchangeRate,D.DocumentGlosa,D.DocumentTotalAmount,
	  DI.DocumentItemRow,DI.CatalogName,DI.DocumentItemBasePrice,
	  DI.DocumentItemQuantity,DI.DocumentItemSubTotalAmount,
	  DI.DocumentItemDiscountAmount,DI.DocumentItemSubTotalNet,
	  DI.DocumentItemTaxAmount,DI.DocumentItemTotalAmount
	 FROM Sale.Document D WITH(NOLOCK)
	 INNER JOIN Sale.DocumentItem DI WITH(NOLOCK) ON D.DocumentID=DI.DocumentID
	 WHERE D.DocumentID=@DocumentID
	SET NOCOUNT OFF 
END