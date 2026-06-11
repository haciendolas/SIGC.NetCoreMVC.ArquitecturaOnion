/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            03/06/2026
   Description:            Permite crear un registro en la tabla Organization.Warehouse
   Execute:	
		  DECLARE @WarehouseID INT,@RetMsg VARCHAR(11)   
		  EXECUTE Organization.uspWarehouseCreate
		    @CompanyID = 1, 
			@EstablishmentID = 4, 
			@WarehouseID=@WarehouseID OUTPUT,	
			@WarehouseTypeID = 1,	
			@WarehouseCode ='0000',	 
			@WarehouseName='ALMACEN 002',
			@WarehouseAddress = 'AV MIRAFLORES - LIMA',			 
			@RecordOriginID = 1,
			@RecordStateID=1,
			@WarehouseCreatedUserID= 1,
			@WarehouseCreatedUserName = 'administrador',
			@WarehouseCreatedUserFullName = 'Joel Castillo',
			@WarehouseCreatedDateTime = '2025-09-02 11:00',
			@RetMsg=@RetMsg OUTPUT	

	 SELECT @WarehouseID AS WarehouseID ,@RetMsg AS RetMsg				   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Organization.uspWarehouseCreate
(  @WarehouseID INT OUTPUT,
   @CompanyID INT,
   @EstablishmentID INT,
   @WarehouseTypeID TINYINT,
   @WarehouseCode VARCHAR(10),  
   @WarehouseName NVARCHAR(50),
   @WarehouseAddress NVARCHAR(150), 
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @WarehouseCreatedUserID INT,
   @WarehouseCreatedUserName NVARCHAR(20),
   @WarehouseCreatedUserFullName NVARCHAR(80),
   @WarehouseCreatedDateTime DATETIME,
   @RetMsg VARCHAR(11) OUTPUT
)
AS
BEGIN 
  EXEC Organization.uspWarehouseVerifyName
		    @CompanyID = @CompanyID,
			@EstablishmentID = @EstablishmentID,
			@WarehouseID=0,
			@WarehouseName=@WarehouseName,		  
		    @RetMsg=@RetMsg OUTPUT		
	
 SET @WarehouseID  = 0			
 IF(@RetMsg = 'OK')
 BEGIN
	  INSERT INTO Organization.Warehouse(CompanyID,EstablishmentID,WarehouseTypeID,WarehouseCode,
			 WarehouseName,WarehouseAddress,RecordOriginID,RecordStateID,WarehouseCreatedUserID,
			 WarehouseCreatedUserName,WarehouseCreatedUserFullName,WarehouseCreatedDateTime
		  )
	  VALUES(@CompanyID,@EstablishmentID,@WarehouseTypeID,@WarehouseCode,
			 @WarehouseName,@WarehouseAddress,@RecordOriginID,@RecordStateID,@WarehouseCreatedUserID,
			 @WarehouseCreatedUserName,@WarehouseCreatedUserFullName,@WarehouseCreatedDateTime
		   )
	 SET @WarehouseID = SCOPE_IDENTITY()
 END
END