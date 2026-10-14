 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            12/10/2026
-- Description:            Permite listar impuesto por catalog de la tabla Product.CatalogStock
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Product.uspCatalogStockList @CompanyID=1,@CatalogID=1,@EstablishmentID=NULL
-- ============================================================================== 
ALTER PROCEDURE Product.uspCatalogStockList(
 @CompanyID INT,
 @CatalogID INT,
 @EstablishmentID INT
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT CS.CatalogStockID,CS.CatalogVariantID,CV.CatalogVariantName,E.EstablishmentID,E.EstablishmentName,
		CS.WarehouseID,W.WarehouseCode, W.WarehouseName,CS.CatalogStockMinimumQuantity,
		CS.CatalogStockMaximumQuantity,CS.CatalogStockCurrentQuantity
	    FROM Product.CatalogStock CS  WITH(NOLOCK) 
		INNER JOIN Product.CatalogVariant CV WITH(NOLOCK) ON CS.CatalogVariantID=CV.CatalogVariantID AND CS.CompanyID=CV.CompanyID AND CV.RecordStateID<>2
		INNER JOIN Organization.Warehouse W WITH(NOLOCK) ON CS.WarehouseID=W.WarehouseID AND CS.CompanyID=W.CompanyID AND W.RecordStateID<>2
		INNER JOIN Organization.Establishment E WITH(NOLOCK) ON W.EstablishmentID=E.EstablishmentID AND W.CompanyID=E.CompanyID AND E.RecordStateID<>2	 
		WHERE CS.CompanyID=@CompanyID 
		AND CV.CatalogID=@CatalogID
		AND (@EstablishmentID IS NULL OR @EstablishmentID=0 OR E.EstablishmentID=@EstablishmentID)
		AND CS.RecordStateID<>2	
		ORDER BY CS.CatalogStockID DESC
	SET NOCOUNT OFF
END