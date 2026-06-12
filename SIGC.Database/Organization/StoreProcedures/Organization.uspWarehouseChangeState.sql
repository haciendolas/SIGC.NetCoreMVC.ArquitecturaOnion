/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            11/06/2026
   Description:            Permite cambiar el estado un registro de la tabla Organization.Warehouse
   Execute:

		  EXECUTE Organization.uspWarehouseChangeState 
		    @CompanyID=1,
			@WarehouseID=2, 
			@RecordStateID=0,
			@WarehouseUpdatedUserID= 1,
			@WarehouseUpdatedUserName = 'administrador',
			@WarehouseUpdatedUserFullName = 'Joel Castillo',
			@WarehouseUpdatedDateTime = '2025-09-02 11:00'							   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Organization.uspWarehouseChangeState
( 
   @CompanyID INT,
   @WarehouseID INT,
   @RecordStateID TINYINT,
   @WarehouseUpdatedUserID INT,
   @WarehouseUpdatedUserName VARCHAR(20),
   @WarehouseUpdatedUserFullName VARCHAR(80),
   @WarehouseUpdatedDateTime DATETIME
)
AS
BEGIN 
    UPDATE Organization.Warehouse SET RecordStateID = @RecordStateID	,
						  WarehouseUpdatedUserID = @WarehouseUpdatedUserID,
			              WarehouseUpdatedUserName = @WarehouseUpdatedUserName,
						  WarehouseUpdatedUserFullName = @WarehouseUpdatedUserFullName,
						  WarehouseUpdatedDateTime = @WarehouseUpdatedDateTime                            
	       WHERE WarehouseID = @WarehouseID
		     AND CompanyID = @CompanyID
END