/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            25/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogMovement
   Execute:	  

		  DECLARE @CatalogMovementID INT  
		  EXECUTE Product.uspCatalogMovementCreate 
			@CatalogMovementID=@CatalogMovementID OUTPUT,
			@CompanyID=1,	
			@CatalogVariantID=1,		  		
			@CatalogLotID=1,
			@WarehouseID=1,	
			@CatalogMovementDate='2026-07-25',
			@CatalogMovementQuantity = 100,		
			@CatalogMovementType = 'IN',
			@ReasonTypeID=1,
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogMovementCreatedUserID=1,
			@CatalogMovementCreatedUserName='administrador',
			@CatalogMovementCreatedUserFullName='Joel Castillo Rojas',
			@CatalogMovementCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogMovementID AS CatalogMovementID	 	   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogMovementCreate
(  @CatalogMovementID INT OUTPUT,
   @CompanyID INT,  
   @CatalogVariantID INT,
   @CatalogLotID INT,
   @WarehouseID INT,
   @CatalogMovementDate DATE,
   @CatalogMovementQuantity NUMERIC(12,6),
   @CatalogMovementType VARCHAR(10),
   @ReasonTypeID TINYINT,
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogMovementCreatedUserID INT,
   @CatalogMovementCreatedUserName NVARCHAR(20),
   @CatalogMovementCreatedUserFullName NVARCHAR(80),
   @CatalogMovementCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogMovement(
	   CompanyID,  
	   CatalogVariantID,
	   CatalogLotID,
	   WarehouseID,
	   CatalogMovementDate,
	   CatalogMovementQuantity,
	   CatalogMovementType,
	   ReasonTypeID,
	   RecordOriginID,
	   RecordStateID,
	   CatalogMovementCreatedUserID,
	   CatalogMovementCreatedUserName,
	   CatalogMovementCreatedUserFullName,
	   CatalogMovementCreatedDateTime)
  VALUES(
	   @CompanyID,  
	   @CatalogVariantID,
	   @CatalogLotID,
	   @WarehouseID,
	   @CatalogMovementDate,
	   @CatalogMovementQuantity,
	   @CatalogMovementType,
	   @ReasonTypeID,
	   @RecordOriginID,
	   @RecordStateID,
	   @CatalogMovementCreatedUserID,
	   @CatalogMovementCreatedUserName,
	   @CatalogMovementCreatedUserFullName,
	   @CatalogMovementCreatedDateTime
  )

 SET @CatalogMovementID = SCOPE_IDENTITY()
END