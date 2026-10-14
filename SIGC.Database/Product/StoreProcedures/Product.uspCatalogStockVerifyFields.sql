/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            07/10/2026
   Description:            Permite verificar un registro de las columnas en la tabla Product.CatalogStock
   Execute:	
		  DECLARE @RetMsg VARCHAR(20)  
		  EXECUTE Product.uspCatalogStockVerifyFields
		    @CatalogStockID=0,
			@CompanyID = 1,
			@CatalogVariantID=1,			
			@WarehouseID = 1,		 	  
		    @RetMsg=@RetMsg OUTPUT										 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE Product.uspCatalogStockVerifyFields
   @CatalogStockID INT, 
   @CompanyID INT,  
   @CatalogVariantID INT,
   @WarehouseID INT, 
   @RetMsg VARCHAR(20) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
     SET @RetMsg='OK'  
    
	 IF EXISTS(SELECT 1 FROM Product.CatalogStock CS WHERE CS.CompanyID = @CompanyID AND
				  CS.CatalogVariantID=@CatalogVariantID AND 
				  CS.WarehouseID=@WarehouseID AND 
				  CS.CatalogStockID<>@CatalogStockID AND
				  CS.RecordStateID<>2
	  )
	   BEGIN 
		SET @RetMsg = 'VARIANT_EXISTS'
	   END
  SET NOCOUNT OFF
END