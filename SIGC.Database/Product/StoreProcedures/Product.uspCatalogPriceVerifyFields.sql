/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            03/10/2026
   Description:            Permite verificar un registro de las columnas en la tabla Product.CatalogPrice
   Execute:	
		  DECLARE @RetMsg VARCHAR(20)  
		  EXECUTE Product.uspCatalogPriceVerifyFields
		    @CatalogPriceID=0,
			@CompanyID = 1,
			@CatalogPresentationID=1,			
			@EstablishmentID = 1,
			@PriceTypeID = 1,		 		  
		    @RetMsg=@RetMsg OUTPUT	
									 
		  SELECT @RetMsg AS 'Message'						   				 
		
   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/ 
 ALTER PROCEDURE Product.uspCatalogPriceVerifyFields
   @CatalogPriceID INT, 
   @CompanyID INT,  
   @CatalogPresentationID INT,
   @EstablishmentID INT,
   @PriceTypeID TINYINT,   
   @RetMsg VARCHAR(20) OUTPUT
AS
BEGIN   
  SET NOCOUNT ON;
     SET @RetMsg='OK'  
    
	 IF EXISTS(SELECT 1 FROM Product.CatalogPrice CP WHERE CP.CompanyID = @CompanyID AND
				  CP.CatalogPresentationID=@CatalogPresentationID AND 
				  CP.EstablishmentID=@EstablishmentID AND
				  CP.PriceTypeID=@PriceTypeID AND
				  CP.CatalogPriceID<>@CatalogPriceID AND
				  CP.RecordStateID<>2
	  )
	   BEGIN 
		SET @RetMsg = 'FIELDS_EXISTS'
	   END
  SET NOCOUNT OFF
END