
/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:           03/06/2026
   Description:            Permite verificar el nombre del almacen en la tabla Organization.Warehouse
   Execute:	
		  DECLARE @RetMsg VARCHAR(11)  
		  EXECUTE Organization.uspWarehouseVerifyName
		    @CompanyID = 1,
			@WarehouseID = 1,
			@EstablishmentID=4,
			@EstablishmentName='Tienda 1',		  
		    @RetMsg=@RetMsg OUTPUT							 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE Organization.uspWarehouseVerifyName
   @CompanyID INT,
   @WarehouseID INT,
   @EstablishmentID INT,   
   @WarehouseName VARCHAR(50),
   @RetMsg VARCHAR(11) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
    SET @RetMsg='OK'
	IF EXISTS(SELECT W.WarehouseID FROM Organization.Warehouse W WHERE W.WarehouseName=@WarehouseName
	     AND W.CompanyID=@CompanyID
	     AND W.EstablishmentID = @EstablishmentID 
		 AND W.WarehouseID<>@WarehouseID
		 AND W.RecordStateID<>2
	)
	BEGIN	  
	  SET @RetMsg = 'NAME_EXISTS'
	END	 
  SET NOCOUNT OFF;
END