/*=============================================================================          
   Author:                 JOEL CASTILLO ROJAS      
   Create date:            08/09/2026
   Description:            Permite actualizar un registro en la tabla Product.CatalogPrice
   Execute:	 
		  EXECUTE Product.uspCatalogPriceUpdate 
			@CatalogPriceID=1,
			@CompanyID=1,	
			@CatalogPresentationID=1,
		    @EstablishmentID=1,
			@PriceTypeID=1,	
			@CurrencyTypeID = 1,	
			@CatalogPriceAmount = 12,
			@CatalogPriceIsTaxIncluded= 1,	
			@RecordStateID=1,
			@CatalogPriceUpdatedUserID=1,
			@CatalogPriceUpdatedUserName='administrador',
			@CatalogPriceUpdatedUserFullName='Joel Castillo Rojas',
			@CatalogPriceUpdatedDateTime='2025-09-02 11:00' 

   Identifcador:		   Date Update  |   User Update   |  Description Update  
     @1
==============================================================================*/
CREATE PROCEDURE Product.uspCatalogPriceUpdate
(  @CatalogPriceID INT ,
   @CompanyID INT,  
   @CatalogPresentationID INT,
   @EstablishmentID INT, 
   @PriceTypeID TINYINT,
   @CurrencyTypeID TINYINT,
   @CatalogPriceAmount NUMERIC(12,6),  
   @CatalogPriceIsTaxIncluded BIT,   
   @RecordStateID TINYINT,
   @CatalogPriceUpdatedUserID INT,
   @CatalogPriceUpdatedUserName NVARCHAR(20),
   @CatalogPriceUpdatedUserFullName NVARCHAR(80),
   @CatalogPriceUpdatedDateTime DATETIME
)
AS
BEGIN 
  UPDATE Product.CatalogPrice SET 
     CatalogPresentationID=@CatalogPresentationID,
	 EstablishmentID = @EstablishmentID,	 
	 PriceTypeID = @PriceTypeID,  
	 CurrencyTypeID = @CurrencyTypeID,
	 CatalogPriceAmount = @CatalogPriceAmount, 
	 CatalogPriceIsTaxIncluded = @CatalogPriceIsTaxIncluded,
	 RecordStateID = @RecordStateID,
	 CatalogPriceCreatedUserID= @CatalogPriceUpdatedUserID,
	 CatalogPriceCreatedUserName=@CatalogPriceUpdatedUserName,
	 CatalogPriceCreatedUserFullName= @CatalogPriceUpdatedUserFullName,
	 CatalogPriceCreatedDateTime = @CatalogPriceUpdatedDateTime  
  WHERE CompanyID = @CompanyID AND 
	    CatalogPriceID = @CatalogPriceID 
END