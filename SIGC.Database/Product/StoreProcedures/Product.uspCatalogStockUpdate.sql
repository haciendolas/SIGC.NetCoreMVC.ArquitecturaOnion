/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            12/10/2026
   Description:            Permite actualizar un registro en la tabla Product.CatalogStock
   Execute:	  

		  EXECUTE Product.uspCatalogStockUpdate 
			@CatalogStockID=1,
			@CompanyID=1,	
			@CatalogVariantID=1,		  		
			@WarehouseID=1,
			@CatalogStockCurrentQuantity = 200,	
			@CatalogStockMinimumQuantity = 10,		
			@CatalogStockMaximumQuantity = 300,
			@CatalogStockUpdatedUserID=1,
			@CatalogStockUpdatedUserName='administrador',
			@CatalogStockUpdatedUserFullName='Joel Castillo Rojas',
			@CatalogStockUpdatedDateTime='2025-09-02 11:00' 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1 
==============================================================================*/
ALTER PROCEDURE Product.uspCatalogStockUpdate
(  @CatalogStockID INT,
   @CompanyID INT,  
   @CatalogVariantID INT,
   @WarehouseID INT,
   @CatalogStockCurrentQuantity NUMERIC(12,6), 
   @CatalogStockMinimumQuantity NUMERIC(12,6),
   @CatalogStockMaximumQuantity NUMERIC(12,6), 
   @CatalogStockUpdatedUserID INT,
   @CatalogStockUpdatedUserName NVARCHAR(20),
   @CatalogStockUpdatedUserFullName NVARCHAR(80),
   @CatalogStockUpdatedDateTime DATETIME
)
AS
BEGIN 
  UPDATE Product.CatalogStock SET 
	 CatalogVariantID = @CatalogVariantID,
	 WarehouseID = @WarehouseID,
	 CatalogStockCurrentQuantity = CASE 
	                                 WHEN @CatalogStockCurrentQuantity IS NULL
									   OR @CatalogStockCurrentQuantity = 0 
									  THEN CatalogStockCurrentQuantity 
									   ELSE @CatalogStockCurrentQuantity 
									END,
	 CatalogStockMinimumQuantity = @CatalogStockMinimumQuantity,
	 CatalogStockMaximumQuantity = @CatalogStockMaximumQuantity,
	 CatalogStockUpdatedUserID = @CatalogStockUpdatedUserID,
	 CatalogStockUpdatedUserName = @CatalogStockUpdatedUserName,
	 CatalogStockUpdatedUserFullName = @CatalogStockUpdatedUserFullName,
	 CatalogStockUpdatedDateTime =@CatalogStockUpdatedDateTime
   WHERE CompanyID= @CompanyID AND	 
	     CatalogStockID= @CatalogStockID 
END