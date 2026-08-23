 -- =============================================================================          
-- Author:                 JOEL CASTILLO ROJAS      
-- Create date:            23/08/2026
-- Description:            Permite obtener listado de almacenes de la tabla  Organization.Warehouse
-- Update:				   Joel Castillo Rojas    
-- Exec                    Exec Organization.uspWarehouseList @CompanyID=1,@EstablishmentID=4
-- ============================================================================== 
CREATE PROCEDURE Organization.uspWarehouseList
 @CompanyID INT,
 @EstablishmentID INT
AS
BEGIN
 SET NOCOUNT ON   
   SELECT W.WarehouseID,W.WarehouseCode,W.WarehouseName
    FROM Organization.Warehouse W WITH(NOLOCK) 
    WHERE W.CompanyID = @CompanyID
	  AND W.EstablishmentID = @EstablishmentID  
	  AND W.RecordStateID = 1 	         
 SET NOCOUNT OFF
END