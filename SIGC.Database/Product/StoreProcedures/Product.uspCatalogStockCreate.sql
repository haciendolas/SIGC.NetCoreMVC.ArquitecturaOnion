/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            25/07/2026
   Description:            Permite crear un registro en la tabla Product.CatalogLot
   Execute:	  

		  DECLARE @CatalogStockID INT  
		  EXECUTE Product.uspCatalogStockCreate 
			@CatalogStockID=@CatalogStockID OUTPUT,
			@CompanyID=1,	
			@CatalogVariantID=1,		  		
			@WarehouseID=1,
			@CatalogStockCurrentQuantity=200,	
			@CatalogStockMinimumQuantity = 10,		
			@CatalogStockMaximumQuantity = 300,
			@RecordOriginID=1,
			@RecordStateID=1,
			@CatalogStockCreatedUserID=1,
			@CatalogStockCreatedUserName='administrador',
			@CatalogStockCreatedUserFullName='Joel Castillo Rojas',
			@CatalogStockCreatedDateTime='2025-09-02 11:00'

		  SELECT @CatalogStockID AS CatalogStockID	 	   				 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogStockCreate
(  @CatalogStockID INT OUTPUT,
   @CompanyID INT,  
   @CatalogVariantID INT,
   @WarehouseID INT,
   @CatalogStockCurrentQuantity NUMERIC(12,6), 
   @CatalogStockMinimumQuantity NUMERIC(12,6),
   @CatalogStockMaximumQuantity NUMERIC(12,6),
   @RecordOriginID TINYINT,
   @RecordStateID TINYINT,
   @CatalogStockCreatedUserID INT,
   @CatalogStockCreatedUserName NVARCHAR(20),
   @CatalogStockCreatedUserFullName NVARCHAR(80),
   @CatalogStockCreatedDateTime DATETIME
)
AS
BEGIN 
  INSERT INTO Product.CatalogStock(
     CompanyID,	 
	 CatalogVariantID,
	 WarehouseID,
	 CatalogStockCurrentQuantity,
	 CatalogStockMinimumQuantity,
	 CatalogStockMaximumQuantity,
     RecordOriginID,
	 RecordStateID,
	 CatalogStockCreatedUserID,
	 CatalogStockCreatedUserName,
	 CatalogStockCreatedUserFullName,
	 CatalogStockCreatedDateTime)
  VALUES(
     @CompanyID,	 
	 @CatalogVariantID,
	 @WarehouseID,
	 @CatalogStockCurrentQuantity,
	 @CatalogStockMinimumQuantity,
	 @CatalogStockMaximumQuantity,
     @RecordOriginID,
	 @RecordStateID,
	 @CatalogStockCreatedUserID,
	 @CatalogStockCreatedUserName,
	 @CatalogStockCreatedUserFullName,
	 @CatalogStockCreatedDateTime
  )

 SET @CatalogStockID = SCOPE_IDENTITY()
END