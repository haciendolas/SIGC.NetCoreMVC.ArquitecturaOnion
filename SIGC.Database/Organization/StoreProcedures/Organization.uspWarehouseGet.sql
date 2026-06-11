 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            10/06/2026
-- Description:            Permite obtener un almacén de la tabla Organization.Warehouse
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Organization.uspWarehouseGet  @CompanyID=1 ,@WarehouseID=1
-- ============================================================================== 
ALTER PROCEDURE Organization.uspWarehouseGet(
   @CompanyID INT,
   @WarehouseID INT 
)
AS
BEGIN
	SET NOCOUNT ON
		SELECT A.WarehouseID,A.EstablishmentID,A.WarehouseTypeID,A.WarehouseCode,A.WarehouseName,A.WarehouseAddress,A.RecordStateID		 
		FROM Organization.Warehouse A WITH(NOLOCK)		 
		WHERE A.CompanyID = @CompanyID AND A.WarehouseID=@WarehouseID 
	SET NOCOUNT OFF
END 