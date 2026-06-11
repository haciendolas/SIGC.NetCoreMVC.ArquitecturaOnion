/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            10/06/2026
   Description:            Permite actualizar un registro en la tabla Organization.Warehouse
   Execute:	
		  DECLARE @RetMsg VARCHAR(11)   
		  EXECUTE Organization.uspWarehouseUpdate
		    @CompanyID = 1, 
			@EstablishmentID = 4, 
			@WarehouseID=1,	
			@WarehouseTypeID = 1,	
			@WarehouseCode ='0001',	 
			@WarehouseName='ALMACEN 002',
			@WarehouseAddress = 'AV MIRAFLORES - LIMA',
			@RecordStateID=1,
			@WarehouseUpdatedUserID= 1,
			@WarehouseUpdatedUserName = 'administrador',
			@WarehouseUpdatedUserFullName = 'Joel Castillo',
			@WarehouseUpdatedDateTime = '2025-09-02 11:00',
			@RetMsg=@RetMsg OUTPUT	

	 SELECT @RetMsg AS RetMsg				   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Organization.uspWarehouseUpdate
(  @WarehouseID INT,
   @CompanyID INT,
   @EstablishmentID INT,
   @WarehouseTypeID TINYINT,
   @WarehouseCode VARCHAR(10),  
   @WarehouseName NVARCHAR(50),
   @WarehouseAddress NVARCHAR(150),   
   @RecordStateID TINYINT,
   @WarehouseUpdatedUserID INT,
   @WarehouseUpdatedUserName NVARCHAR(20),
   @WarehouseUpdatedUserFullName NVARCHAR(80),
   @WarehouseUpdatedDateTime DATETIME,
   @RetMsg VARCHAR(11) OUTPUT
)
AS
BEGIN 
  EXEC Organization.uspWarehouseVerifyName
		    @CompanyID = @CompanyID,
			@EstablishmentID = @EstablishmentID,
			@WarehouseID=@WarehouseID,
			@WarehouseName=@WarehouseName,		  
		    @RetMsg=@RetMsg OUTPUT
 	
 IF(@RetMsg = 'OK')
  BEGIN
	  UPDATE Organization.Warehouse SET 
	        EstablishmentID = @EstablishmentID,
		    WarehouseTypeID = @WarehouseTypeID,
			WarehouseCode = @WarehouseCode,
			WarehouseName = @WarehouseName,
			WarehouseAddress = @WarehouseAddress,
			RecordStateID = @RecordStateID,
			WarehouseUpdatedUserID = @WarehouseUpdatedUserID,
			WarehouseUpdatedUserName = @WarehouseUpdatedUserName,
			WarehouseUpdatedUserFullName = @WarehouseUpdatedUserFullName,
			WarehouseUpdatedDateTime = @WarehouseUpdatedDateTime		  
	  WHERE CompanyID = @CompanyID AND
			WarehouseID = @WarehouseID 
   END
END